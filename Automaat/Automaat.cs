using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaat
{
    public class Automaat<T> where T : IComparable
    {

        private readonly HashSet<Transition<T>> _transitions;
        private readonly SortedSet<T> _states;
        private readonly SortedSet<T> _startStates;
        private readonly SortedSet<T> _finalStates;
        private SortedSet<char> _symbols;

        public Automaat(): this(new SortedSet<char>())
        { }

        public Automaat(char[] s) : this(new SortedSet<char>(s))
        { }

        public Automaat(SortedSet<char> symbols)
        {
            _transitions = new HashSet<Transition<T>>();
            _states = new SortedSet<T>();
            _startStates = new SortedSet<T>();
            _finalStates = new SortedSet<T>();

            SetAlphabet(symbols);
        }

        public void SetAlphabet(char [] s)
        {
            SetAlphabet(new SortedSet<char>(s));
        }

        public void SetAlphabet(SortedSet<char> symbols)
        {
            _symbols = symbols;
        }

        public SortedSet<char> GetAlphabet()
        {
            return _symbols;
        }

        public void AddTransition(Transition<T> t)
        {
            _transitions.Add(t);
            _states.Add(t.FromState);
            _states.Add(t.ToState);
        }

        public void DefineAsStartState(T t)
        {
            _states.Add(t);
            _startStates.Add(t);
        }

        public void DefineAsFinalState(T t)
        {
            _states.Add(t);
            _finalStates.Add(t);
        }

        public void PrintTransitions()
        {
            foreach (Transition<T> t in _transitions) {
                Console.WriteLine(t.ToString());
            }
        }

        public bool IsDfa()
        {
            bool isDfa = true;

            foreach (T from in _states)
            {
                foreach (char symbol in _symbols)
                {
                    // isDFA = isDFA && getToStates(from, symbol).size() == 1;
                    isDfa = isDfa && GetToStates(from, symbol).Count == 1;
                }
            }

            return isDfa;
        }

        public List<T> GetToStates(T from, char symbol)
        {
            List<T> toStates = new List<T>();

            foreach(Transition<T> trans in _transitions)
            {
                if (trans.FromState.Equals(from) && trans.Symbol.Equals(symbol))
                {
                    toStates.Add(trans.ToState);
                }
            }

            return toStates;
        }

        public bool Accepteer(string s)
        {
            char[] symbols = s.ToCharArray();
            foreach (char sym in symbols)
            {
                Console.Write(sym);
                if (!_symbols.Contains(sym)) { return false; }
            }
            Console.WriteLine();

            if (!IsDfa())
            {
                return false;
            }

            var finalState = GetFinalState(symbols);
            return _finalStates.Contains(finalState);
        }

        private T GetFinalState(char[] symbols)
        {
            var startState = _startStates.First();
            var finalState = GetNextState(startState, symbols);
            return finalState;
        }

        private T GetNextState(T currentState, char[] leftoverSymbols)
        {
            if (leftoverSymbols.Length == 0) return currentState;

            char nextSymbol = leftoverSymbols.First();
            Transition<T> trans = GetTransition(currentState, nextSymbol);

            leftoverSymbols = leftoverSymbols.Skip(1).ToArray();
           
            if (trans == null) return GetNextState(currentState, leftoverSymbols);
            return GetNextState(trans.ToState, leftoverSymbols);
        }       
        
        private Transition<T> GetTransition(T state, char symbol)
        {
            foreach (Transition<T> trans in _transitions)
            {
                if (trans.FromState.Equals(state) && trans.Symbol.Equals(symbol))
                {
                    return trans;
                }
            }
            return null;
        } 
    }
}
