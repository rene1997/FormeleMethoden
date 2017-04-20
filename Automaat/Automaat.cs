using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class Automaat<T> where T : IComparable
    {

        private HashSet<Transition<T>> transitions;
        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        public SortedSet<char> symbols;

        public Automaat(): this(new SortedSet<char>())
        { }

        public Automaat(char[] s) : this(new SortedSet<char>(s))
        { }

        public Automaat(SortedSet<char> symbols)
        {
            transitions = new HashSet<Transition<T>>();
            states = new SortedSet<T>();
            startStates = new SortedSet<T>();
            finalStates = new SortedSet<T>();

            setAlphabet(symbols);
        }

        private void setAlphabet(char [] s)
        {
            setAlphabet(new SortedSet<char>(s));
        }

        private void setAlphabet(SortedSet<char> symbols)
        {
            this.symbols = symbols;
        }

        public SortedSet<char> setAlphabet()
        {
            return symbols;
        }

        public void addTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.fromState);
            states.Add(t.toState);
        }

        public void defineAsStartState(T t)
        {
            states.Add(t);
            startStates.Add(t);
        }

        public void defineAsFinalState(T t)
        {
            states.Add(t);
            finalStates.Add(t);
        }

        public void printTransitions()
        {
            foreach (Transition<T> t in transitions) {
                Console.WriteLine(t.ToString());
            }
        }

        public bool isDFA()
        {
            bool isDFA = true;

            foreach (T from in states)
            {
                foreach (char symbol in symbols)
                {
                    // isDFA = isDFA && getToStates(from, symbol).size() == 1;
                    isDFA = isDFA && getToStates(from, symbol).Count == 1;
                }
            }

            return isDFA;
        }

        public List<T> getToStates(T from, char symbol)
        {
            List<T> toStates = new List<T>();

            foreach(Transition<T> trans in transitions)
            {
                if (trans.fromState.Equals(from) && trans.symbol.Equals(symbol))
                {
                    toStates.Add(trans.toState);
                }
            }

            return toStates;
        }

        public bool accepteer(string s)
        {
            return true;
        }
    }
}
