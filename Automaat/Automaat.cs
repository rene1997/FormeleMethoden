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

        public List<string> GeefTaal(int length)
        {
            var allWords = new List<string>();
            MakeWords(string.Empty, length, ref allWords);
            return allWords;
        }

        public void Minimize()
        {
            RemoveStates();
            var blockTable = new HashSet<Block>();

            //make final state block
            var finalStateBlock = new Block() { isFinalState = true, rows = new SortedSet<Tuple<T, SortedSet<Tuple<char, T>>>>() };
            foreach (var finalState in _finalStates)
            {
                var destinations = new SortedSet<Tuple<char, T>>();
                foreach (var symbol in _symbols)
                {
                    destinations.Add(new Tuple<char, T>(symbol, GetToStates(finalState, symbol).First()));
                }
                finalStateBlock.rows.Add(new Tuple<T, SortedSet<Tuple<char, T>>>(finalState, destinations));                    
            }
            
            //make first block
            var aBlock = new Block() { isFinalState = false, rows = new SortedSet<Tuple<T, SortedSet<Tuple<char, T>>>>() };
            foreach (var state in _states)
            {
                if(IsFinalState(state)) continue;
                var destinations = new SortedSet<Tuple<char, T>>();
                foreach(var symbol in _symbols)
                {
                    destinations.Add(new Tuple<char, T>(symbol, GetToStates(state, symbol).First()));
                }
                aBlock.rows.Add(new Tuple<T, SortedSet<Tuple<char, T>>>(state, destinations));
            }

            blockTable.Add(finalStateBlock);
            blockTable.Add(aBlock);
            Console.WriteLine("first table created, going to minimize this shit");
            MinimizeHopcroft(ref blockTable);
        }

        private class Block
        {
            public bool isFinalState = false;
            public bool isMinimized = false;
            public SortedSet<Tuple<T,SortedSet<Tuple<char, T>>>> rows = new SortedSet<Tuple<T, SortedSet<Tuple<char, T>>>>();

            public void SetMinimized(bool isMinimized)
            {
                this.isMinimized = isMinimized;
            }
        }

        private void MinimizeHopcroft(ref HashSet<Block> blockTable)
        {
            if(CheckMinimized(ref blockTable)) return;

            var newBlocks = new List<Block>();
            for (var i = blockTable.Count -1; i >= 0; i--)
            {
                var block = blockTable.ElementAt(i);
                if (block.isMinimized) continue;
                var perfectRow = block.rows.First();
                for(var j = block.rows.Count -1; j > 0; j--)
                {
                    var row = block.rows.ElementAt(j);
                    //check if row has same destinations as perfect row
                    if(!CompareRows(perfectRow, row)){
                        //add to new block
                    }
                }
                

            }
        }

        private bool CompareRows(Tuple<T, SortedSet<Tuple<char, T>>> item1, Tuple<T, SortedSet<Tuple<char, T>>> item2)
        {
            var isEquals = true;
            foreach(var symbol in item1.Item2)
            {
                
            }

            return isEquals;
        }

        private bool CheckMinimized(ref HashSet<Block> blockTable)
        {
            for (var i = 0; i < blockTable.Count; i++)
            {
                var block = blockTable.ElementAt(i);
                var destinations = new HashSet<Tuple<char, int>>();
                foreach (var row in block.rows)
                    foreach (var symbol in row.Item2)
                        destinations.Add(new Tuple<char, int>(symbol.Item1, GetBlock(blockTable, symbol.Item2)));
                block.SetMinimized(destinations.Count.Equals(_symbols.Count));
            }
            foreach(var b in blockTable)
            {
                if (!b.isMinimized) return false;
            }
            return true;
        }

        private int GetBlock(HashSet<Block> blockTable, T state)
        {
            for(var i = 0; i < blockTable.Count;i ++)
            {
                var block = blockTable.ElementAt(i);
                foreach(var row in block.rows)
                {
                    if (row.Item1.Equals(state))
                        return i;
                }
            }
            return -1; 
        }

        //private Block getBlock(HashSet<Block> table, T state)
        //{
        //    var destinations = new HashSet<T>();
        //    foreach(var symbol in destinations)
        //    {

        //    }
        //    return table.First();
        //}

        /// <summary>
        /// removes not accessible states
        /// </summary>
        private void RemoveStates()
        {
            for (int i = _states.Count -1; i >= 0; i --)
            {
                bool mustStay = false;
                foreach(var s in _transitions)
                    if (s.ToState.Equals(_states.ElementAt(i))) mustStay = true;

                foreach(var s in _startStates)
                    if (s.Equals(_states.ElementAt(i))) mustStay = true;
                
                if (!mustStay) _states.Remove(_states.ElementAt(i));
            }
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

        private bool IsFinalState(T state)
        {
            foreach (var fstate in _finalStates)
                if (state.Equals(fstate)) return true;
            return false;
        }
    }
}
