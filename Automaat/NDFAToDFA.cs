using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class State<T> where T: IComparable
    {
        public T _state;

        //char is alphabet symbol
        //Sortedset are states where go to
        public SortedSet<Tuple<char, T>> toStates;

        public State(T state)
        {
            _state = state;
            toStates = new SortedSet<Tuple<char, T>>();
        }
    }


    public class NDFAToDFA<T> where T: IComparable
    {
        public static Automaat<T> MakeDFA<T>(Automaat<T> ndfa) where T : IComparable
        {
            if(ndfa.IsDfa())
                return ndfa;
            var alphabet = ndfa.GetAlphabet();
            
            Automaat<T> dfa = new Automaat<T>(alphabet);
            var table = GetTable(ndfa);
            var stateCounter = 0;
            //Get start states
            var startstate = ndfa._startStates;
            

            return dfa;
        }
        


        private static SortedSet<Tuple<T, char, T>> GetTable<T>(Automaat<T> ndfa) where T : IComparable
        {
            //get transitions (with epsilons)
            var toStates = new SortedSet<Tuple<T,char,T>>();
            foreach (var s in ndfa._states)
            {
                GetToStates(s, ndfa).ToList().ForEach(tuple => toStates.Add(new Tuple<T, char, T>(s, tuple.Item2, tuple.Item1)));
            }

            //put it al together
            var table = MergeDataToTable(toStates);
            PrintTable(table);
            
            //get startstates:


            //get tostates:
            
            //get finalstates:
            return toStates;
        }

        private static SortedSet<Tuple<T,char>> GetToStates<T>(T state, Automaat<T> automaat) where T: IComparable
        {
            var list = new SortedSet<Tuple<T,char>>();
            foreach(var t in automaat._transitions)
            {
                if (t.FromState.Equals(state))
                {
                    if (t.IsEpsilonTransition())
                    {
                        GetToStates(t.ToState, automaat).ToList().ForEach(s => list.Add(s));
                    }
                    else
                    {
                        list.Add(new Tuple<T,char>(t.ToState, t.Symbol));
                    }
                }
            }
            return list;
        }

        private static SortedSet<Tuple<T, char, SortedSet<T>>> MergeDataToTable<T>(SortedSet<Tuple<T, char, T>> tostates) where T : IComparable
        {
            var table = new SortedSet<Tuple<T, char, SortedSet<T>>>();

            foreach(var tuple in tostates)
            {
                bool added = false;
                foreach(var row in table)
                {
                    if (tuple.Item1.Equals(row.Item1) && tuple.Item2 == row.Item2)
                    {
                        row.Item3.Add(tuple.Item3);
                        added = true;
                    }
                }
                if (!added)
                {
                    var newrow = new Tuple<T, char, SortedSet<T>>(tuple.Item1, tuple.Item2, new SortedSet<T>());
                    newrow.Item3.Add(tuple.Item3);
                    table.Add(newrow);
                }
            }
            return table;
        }

        public static void PrintTable<T>(SortedSet<Tuple<T, char, SortedSet<T>>> table)
        {
            Console.WriteLine("state: \t\tsymbol:\t\tDestinations:");
            foreach(var row in table)
            {
                string destinations = String.Empty;
                row.Item3.ToList().ForEach(symbol => destinations += $"{symbol}, ");
                Console.WriteLine($"{row.Item1}\t\t{row.Item2}\t\t{destinations}");
            }
        }

    }
}
