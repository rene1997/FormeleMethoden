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
        


        private static SortedSet<Tuple<T, char, List<T>>> GetTable<T>(Automaat<T> ndfa) where T : IComparable
        {
            var toStates = new SortedSet<Tuple<T,char,List<T>>>();
            //get startstates:
            foreach(var s in ndfa._states)
            {
                var startStates = GetToStates(s, ndfa);
            }
            
            //get tostates:

            //get finalstates:
            return toStates;
        }

        private static SortedSet<T> GetToStates<T>(T state, Automaat<T> automaat) where T: IComparable
        {
            return null;
        }

    }
}
