using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaat
{
    public class NdfatoDfa
    {
        private struct State<T> : IComparable where T : IComparable
        {
            public int Id { get; }
            public SortedSet<T> States { get; set; }

            public State(int id)
            {
                this.Id = id;
                this.States = new SortedSet<T>();
            }

            public int CompareTo(object obj)
            {
                if (!(obj is State<T>)) return -1;

                var other = (State<T>) obj;
                return Id.CompareTo(other.Id);
            }

            public bool HasSameStates(SortedSet<T> states)
            {
                if (states.Count != this.States.Count) return false;
                foreach (var state in states)
                {
                    if (!this.States.Contains(state)) return false;
                }
                return true;
            }
        }
        public static Automaat<int> MakeDfa<T>(Automaat<T> ndfa) where T : IComparable
        {
            var betweenDfa = new Automaat<State<T>>(ndfa.GetAlphabet());
            var table = MakeTable(ndfa);
            // PrintTable(table);
            var stateCounter = 1;

            // Define start state
            var startStates = new SortedSet<T>(ndfa._startStates);
            ndfa._startStates.ToList().ForEach(sState => GetToStatesByEpsilon(ref startStates, ndfa, sState));
            var startState = new State<T>(stateCounter) {States = startStates};
            betweenDfa.DefineAsStartState(startState);

            BuildBetweenDfa(ref betweenDfa, table, ref stateCounter, startState);

            // Define final state
            var finalStates = new SortedSet<State<T>>();
            foreach (var state in betweenDfa._states)
            {
                foreach (var stateInAState in state.States)
                {
                    if (ndfa._finalStates.Contains(stateInAState))
                    {
                        finalStates.Add(state);
                    }
                }
            }
            foreach (var finalState in finalStates)
            {
                betweenDfa.DefineAsFinalState(finalState);
            }
            

            return BuildDfa(betweenDfa);
        }

        private static SortedSet<Tuple<T, char, SortedSet<T>>> MakeTable<T>(Automaat<T> ndfa) where T : IComparable
        {
            var table = new SortedSet<Tuple<T, char, SortedSet<T>>>();
            foreach (var state in ndfa._states)
            {
                foreach (var symbol in ndfa._symbols)
                {
                    var listOfToStates = new SortedSet<T>();
                    FindToStates(ref listOfToStates, ndfa, state, symbol);
                    table.Add(new Tuple<T, char, SortedSet<T>>(state, symbol, listOfToStates));
                }
            }
            return table;
        }
        private static void GetToStatesByEpsilon<T>(ref SortedSet<T> epsilonStates, Automaat<T> ndfa, T t) where T : IComparable
        {
            foreach (var eTrans in ndfa.GetEpsilonTransitions(t))
            {
                epsilonStates.Add(eTrans.ToState);
                GetToStatesByEpsilon(ref epsilonStates, ndfa, eTrans.ToState);
            }
        }
        public static SortedSet<T> FindToStates<T>(ref SortedSet<T> toStates, Automaat<T> ndfa, T state, char symbol) where T : IComparable
        {
            var transitions = ndfa.GetTransitions(state, symbol);
            foreach (var trans in transitions)
            {
                if (trans.IsEpsilonTransition())
                {
                    if (symbol == Transition<T>.Epsilon)
                    {
                        toStates.Add(trans.ToState);
                        FindToStates(ref toStates, ndfa, trans.ToState, Transition<T>.Epsilon);
                    }
                    else
                    {
                        FindToStates(ref toStates, ndfa, trans.ToState, symbol);
                    }
                }
                else
                {
                    toStates.Add(trans.ToState);
                    FindToStates(ref toStates, ndfa, trans.ToState, Transition<T>.Epsilon);
                }
            }
            return toStates;
        }

        private static void BuildBetweenDfa<T>(ref Automaat<State<T>> a, SortedSet<Tuple<T, char, SortedSet<T>>> table, ref int stateCounter, State<T> s) where T : IComparable
        {
            foreach (var symbol in a._symbols)
            {
                var toStates = new SortedSet<T>();
                foreach (var state in s.States)
                {
                    foreach (var record in table)
                    {
                        if (record.Item1.CompareTo(state) == 0 && record.Item2 == symbol)
                        {
                            record.Item3.ToList().ForEach(toState => toStates.Add(toState));
                        }
                    }
                }

                var transToState = s;
                var stateId = stateCounter;
                var stateExsists = false;

                if (toStates.Count == 0)
                {
                    stateId = 0;
                    foreach (var state in a._states)
                    {
                        if (state.Id == stateId)
                        {
                            stateExsists = true;
                            transToState = state;
                            break;
                        }
                    }
                }
                else
                {
                    var isSameState = false;
                    foreach (var state in a._states)
                    {
                        isSameState = state.HasSameStates(toStates);
                        if (isSameState)
                        {
                            stateExsists = true;
                            transToState = state;
                            break;
                        }
                    }
                    
                    if (!isSameState)
                    {
                        stateCounter++;
                        stateId = stateCounter;
                    }
                }

                if (!stateExsists)
                {
                    var newState = new State<T>(stateId) {States = toStates};
                    a.AddTransition(new Transition<State<T>>(s, symbol, newState));
                    BuildBetweenDfa(ref a, table, ref stateCounter, newState);
                }
                else
                {
                    a.AddTransition(new Transition<State<T>>(s, symbol, transToState));
                }
            }
        }

        private static Automaat<int> BuildDfa<T>(Automaat<State<T>> stateAutomaat) where T : IComparable
        {
            var dfa = new Automaat<int>(stateAutomaat.GetAlphabet());
            
            foreach (var startState in stateAutomaat._startStates)
            {
                dfa.DefineAsStartState(startState.Id);
            }
            foreach (var finalState in stateAutomaat._finalStates)
            {
                dfa.DefineAsFinalState(finalState.Id);
            }
           
            foreach (var trans in stateAutomaat._transitions)
            {
                dfa.AddTransition(new Transition<int>(trans.FromState.Id, trans.Symbol, trans.ToState.Id));
            }

            if (!dfa.IsDfa()) throw new Exception("Created dfa is not a dfa :(");
            return dfa;
        }
        
        public static void PrintTable<T>(SortedSet<Tuple<T, char, SortedSet<T>>> table)
        {
            Console.WriteLine("State\tSymbol\tDestination");
            foreach (var record in table)
            {
                Console.Write($"{record.Item1}\t{record.Item2}\t{String.Join(",", record.Item3)}\n");
            }
        }
    }
}
