using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Automaat
{
    public class Automaat<T> where T : IComparable
    {

        public readonly HashSet<Transition<T>> _transitions;
        public readonly SortedSet<T> _states;
        public readonly SortedSet<T> _startStates;
        public readonly SortedSet<T> _finalStates;
        public SortedSet<char> _symbols;

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
            var symbols = s.ToCharArray();
            foreach (char sym in symbols)
            {
                if (!_symbols.Contains(sym)) { return false; }
            }

            var finalStates = new List<T>();
            foreach (var startState in _startStates)
            {
                //Console.WriteLine("cState\tSymbolsOver");
                GetNextStates(startState, symbols, ref finalStates);
            }

            foreach (var finalState in finalStates)
            {
                if (_finalStates.Contains(finalState)) return true;
            }

            return false;
        }
        
        private void GetNextStates(T currentState, char[] leftoverSymbols, ref List<T> finalStates)
        {
            //Console.Write($"{currentState}\t");
            //leftoverSymbols.ToList().ForEach(ob => Console.Write($"{ob},"));
            //Console.WriteLine();

            if (leftoverSymbols.Length == 0)
            {
                finalStates.Add(currentState);
                foreach (var t in GetEpsilonTransitions(currentState))
                {
                    GetNextStates(t.ToState, leftoverSymbols, ref finalStates);
                }
                return;
            }

            var nextSymbol = leftoverSymbols.First();
            var transitions = GetTransitions(currentState, nextSymbol);

            if (transitions.Count == 0)
            {
                GetNextStates(currentState, leftoverSymbols.Skip(1).ToArray(), ref finalStates);
                return;
            }
            
            foreach (var t in transitions)
            {
                if (!t.IsEpsilonTransition())
                {
                    GetNextStates(t.ToState, leftoverSymbols.Skip(1).ToArray(), ref finalStates);
                }
                else
                {
                    GetNextStates(t.ToState, leftoverSymbols, ref finalStates);
                }
            }
        }

        private List<Transition<T>> GetTransitions(T state, char symbol)
        {
            var allTransitions = new List<Transition<T>>();
            foreach (var trans in _transitions)
            {
                if (trans.FromState.Equals(state) && (trans.Symbol.Equals(symbol) || trans.IsEpsilonTransition()))
                {
                    allTransitions.Add(trans);
                }
            }
            return allTransitions;
        }

        private List<Transition<T>> GetEpsilonTransitions(T state)
        {
            var epsilonTransitions = new List<Transition<T>>();
            foreach (var trans in _transitions)
            {
                if (trans.FromState.Equals(state) && trans.IsEpsilonTransition())
                {
                    epsilonTransitions.Add(trans);
                }
            }
            return epsilonTransitions;
        }

        public List<string> GeefTaal(int length)
        {
            var allWords = new List<string>();
            MakeWords(string.Empty, length, ref allWords);
            return allWords;
        }

        private void MakeWords(String word, int length, ref List<string> allWords)
        {
            if (word.Length >= length)
                return;

            for (int i = 0; i < _symbols.Count; i++)
            {
                var newWord = word + _symbols.ElementAt(i);
                Console.WriteLine(newWord);
                if (Accepteer(newWord))
                    allWords.Add(newWord);
                MakeWords(newWord, length, ref allWords);
            }
        }
    }
}
