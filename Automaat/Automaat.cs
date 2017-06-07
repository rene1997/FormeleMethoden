using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

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

        internal void Print()
        {
            string alphabet = string.Empty;
            _symbols.ToList().ForEach(s => alphabet += $"{s}, ");
            alphabet = alphabet.Substring(0, alphabet.Length - 2);
            Console.WriteLine($"alphabet: {alphabet}");
            
            string startstates = string.Empty;
            _startStates.ToList().ForEach(s => startstates += $"{s}, ");
            startstates = startstates.Substring(0, startstates.Length - 2);
            Console.WriteLine($"startstates: {startstates}");

            string finalstates = string.Empty;
            _finalStates.ToList().ForEach(s => finalstates += $"{s}, ");
            finalstates = finalstates.Substring(0, finalstates.Length - 2);
            Console.WriteLine($"finalstates: {finalstates}");

            Console.WriteLine("transitions:");
            PrintTransitions();
        }

        public List<string> GeefTaal(int length)
        {
            var allWords = new List<string>();
            MakeWords(string.Empty, length, ref allWords, true);
            return allWords;
        }

        public List<string> GeefNietTaal(int length)
        {
            var allWords = new List<string>();
            MakeWords(string.Empty, length, ref allWords, false);
            return allWords;
        }

        public override bool Equals(object obj)
        {
            Automaat<T> item2 = (Automaat<T>)obj;
            if (item2 == null) return false;
            //check transitions
            if (item2._transitions.Count != _transitions.Count) return false;
            for (var i = 0; i < _transitions.Count; i++)
            {
                var t1 = _transitions.ElementAt(i);
                var t2 = item2._transitions.ElementAt(i);
                if (!t1.Equals(t2)) return false;
            }

            //check finalstates
            if (item2._finalStates.Count != _finalStates.Count) return false;
            for(var i = 0; i < _finalStates.Count; i++)
            {
                var f1 = _transitions.ElementAt(i);
                var f2 = item2._finalStates.ElementAt(i);
                if (!f1.Equals(f2)) return false;
            }
            return true;
        }

        private void MakeWords(String word, int length, ref List<string> allWords, bool accept)
        {
            if (word.Length >= length)
                return;

            for (int i = 0; i < _symbols.Count; i++)
            {
                var newWord = word + _symbols.ElementAt(i);
                if (Accepteer(newWord) && accept)
                    allWords.Add(newWord);
                else if (!Accepteer(newWord) && !accept)
                    allWords.Add(newWord);
                MakeWords(newWord, length, ref allWords, accept);
            }
        }

        public void ViewImage()
        {
            Graphviz.PrintGraph(this, "test");
        }

        public Automaat<int> MinimizeHopCroft()
        {
            var newAutomaat = NdfatoDfa.MakeDfa(this);
            RemoveStates(); 
            var table = new Table(newAutomaat);
            table.Minimize();
            table.Print();
            return table.ToAutomaat();
        }

        public Automaat<int> MinimizeReverse()
        {
            var automate = NdfatoDfa.MakeDfa(Reverse());
            automate = NdfatoDfa.MakeDfa(automate.Reverse());
            return automate;
        }

        private class Table
        {
            public Automaat<int> Automaat;
            public SortedSet<char> Alphabet = new SortedSet<char>();
            public List<Block> Blocks = new List<Block>();

            public Table(Automaat<int> automaat)
            {
                Automaat = automaat;
                automaat._symbols.ToList().ForEach(s => Alphabet.Add(s));
                InitBlocks();
                Blocks.ForEach(b => b.FindDestinations());
            }
            

            public void Minimize()
            {
                Blocks.ForEach(b => b.FindDestinations());
                var isMinimized = true;
                foreach(var block in Blocks)
                {
                    if (!block.Minimize())
                    {
                        isMinimized = false;
                    }
                }
                if (!isMinimized)
                {
                    for(int i = Blocks.Count -1; i >= 0; i--)
                    {
                        Blocks[i].SplitBlock();
                    }
                }

            }

            public void Print()
            {
                var alphabet = String.Empty;
                Alphabet.ToList().ForEach(c => alphabet += c + ",");
                Console.WriteLine($"table:\nalphabet: {alphabet.Substring(0,alphabet.Length -1)}");
                Blocks.ForEach(b => b.Print());
            }

            public Automaat<int> ToAutomaat()
            {
                var automaat = new Automaat<int>(Alphabet);
                foreach(var block in Blocks)
                {
                    foreach(char c in Alphabet)
                    {
                        automaat.AddTransition(new Transition<int>(block.Identifier, c, block.Rows[0].FindDestination(c).Identifier));
                    }
                    Automaat._startStates.ToList().ForEach(startstate =>
                    {
                        block.States.ToList().ForEach(state =>
                        {
                            if (state.Equals(startstate))
                                automaat.DefineAsStartState(block.Identifier);
                        });
                    });
                    if (block.isFinalState)
                        automaat.DefineAsFinalState(block.Identifier);
                }
                
                return automaat;
            }

            private void InitBlocks()
            {
                //finalStates
                Blocks.Add(new Block(this, Automaat._finalStates, 'A') { isFinalState = true});

                //non finalstates:
                var states = new SortedSet<int>();
                foreach(int state in Automaat._states)
                {
                    var isFinalState = false;
                    Automaat._finalStates.ToList().ForEach(s => { if (s.Equals(state)) isFinalState = true;  });
                    if (!isFinalState) states.Add(state);
                }
                Blocks.Add(new Block(this, states, 'B'));
            }
        }

        private class Block 
        {
            public List<Row> Rows = new List<Row>();
            public char Identifier;
            public SortedSet<int> States = new SortedSet<int>();
            public bool isFinalState { get; set; }
            private Table table;
            private Automaat<int> automaat;
            
            public Block(Table table, List<Row> rows, char id)
            {
                this.table = table;
                this.automaat = table.Automaat;
                this.Identifier = id;
                this.Rows = rows;
                rows.ForEach(r => States.Add(r.State));
            }

            public Block(Table table, SortedSet<int> states, char id)
            {
                this.table = table;
                this.automaat = table.Automaat;
                this.Identifier = id;
                this.States = states;

                var rowId = 0;
                states.ToList().ForEach(x => { Rows.Add(new Row(table, x, rowId));rowId++;});
            }

            public void AddRow(Row row)
            {
                Rows.Add(row);
                States.Add(row.State);
            }

            public void Print()
            {
                Console.WriteLine($"block {Identifier}:");
                Rows.ForEach(r => r.Print());
            }
            
            public void FindDestinations()
            {
                Rows.ForEach(r => r.FindDestinations());
            }

            public bool Minimize()
            {
                bool isMinimized = true;
                foreach(var c in table.Alphabet)
                {
                    var destinations = new HashSet<Block>();
                    Rows.ForEach(r => destinations.Add(r.FindDestination(c)));
                    if(destinations.Count > 1)
                        isMinimized = false;
                }
                return isMinimized;
            }

            public bool CompareRow(Row row)
            {
                bool isSame = true;
                Rows.ForEach(r => { if (!r.Compare(row)) isSame = false; ; });
                return isSame;
                
            }

            public void SplitBlock()
            {
                var newBlocks = new List<Block>();
                var perfectRow = Rows[0];
                for(var i = Rows.Count -1; i > 0; i--)
                {
                    var row = Rows[i];
                    if (!row.Compare(perfectRow)){
                        bool isAdded = false;
                        foreach(var newBlock in newBlocks)
                        {
                            if (newBlock.CompareRow(row))
                            {
                                newBlock.AddRow(row);
                                isAdded = true;
                            }
                        }
                        if (!isAdded)
                        {
                            var rows = new List<Row>();
                            rows.Add(row);
                            var NewBlock = new Block(table, rows, (char)((int)table.Blocks.Last().Identifier + 1));
                            NewBlock.isFinalState = isFinalState;
                            newBlocks.Add(NewBlock);
                        }
                        Rows.RemoveAt(i);
                        States.Remove(row.State);
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
                newBlocks.ForEach(b => table.Blocks.Add(b));
            }
        }

        private class Row 
        {
            public int Identifier;
            public int State;
            public List<Destination> Destinations = new List<Destination>();
            private Table table;

            public Row(Table table, int state, int id)
            {
                this.table = table;
                this.State = state;
                this.Identifier = id;
                
                foreach(var c in table.Alphabet)
                {
                    table.Automaat.GetToStates(state, c).ForEach(s => Destinations.Add(new Destination(table,c,s)));
                }
            }

            public void Print()
            {
                Console.WriteLine($"\trow: {Identifier} \n\t\tstate: {State}");
                Destinations.ForEach(d => d.Print(State));
            }

            public Block FindDestination(char symbol)
            {
                foreach(var d in Destinations)
                {
                    if (d.Symbol.Equals(symbol))
                        return d.TargetBlock;
                }
                return null;
            }

            public void FindDestinations()
            {
                Destinations.ForEach(d => d.FindDestination());
            }

            public bool Compare(Row row)
            {
                foreach(char c in table.Alphabet)
                {
                    var target1 = FindDestination(c);
                    var target2 = row.FindDestination(c);
                    if (!target1.Equals(target2))
                        return false;
                }
                return true;
            }
        }

        private class Destination
        {
            public char Symbol;
            public int Target;
            public Block TargetBlock;
            private Table table;

            public Destination(Table table, char symbol, int target)
            {
                this.table = table;
                Symbol = symbol;
                Target = target;
            }

            public Block FindDestination()
            {
                foreach(var b in table.Blocks)
                {
                    foreach(var state in b.States)
                    {
                        if (state.Equals(Target)) {
                            TargetBlock = b;
                            return b;
                        }
                            
                    }
                }
                return null;
            }

            public void Print(int state)
            {
                if (TargetBlock != null)
                    Console.WriteLine($"\t\tdestination: \tinput:{Symbol} \t target: {Target} / {TargetBlock.Identifier}");
                else
                    Console.WriteLine($"\t\tdestination: \tinput:{Symbol} \t target: {Target}");
            }
        }


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

        public List<Transition<T>> GetTransitions(T state, char symbol)
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

        public List<Transition<T>> GetEpsilonTransitions(T state)
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

        public Automaat<T> Reverse()
        {
            var reversedAutomaat = new Automaat<T>(_symbols);

            _startStates.ToList().ForEach(state => reversedAutomaat.DefineAsFinalState(state));
            _finalStates.ToList().ForEach(state => reversedAutomaat.DefineAsStartState(state));
            _transitions.ToList().ForEach(trans => reversedAutomaat.AddTransition(trans.Reverse()));
            
            return reversedAutomaat;
        }

        private bool IsFinalState(T state)
        {
            foreach (var fstate in _finalStates)
                if (state.Equals(fstate)) return true;
            return false;
        }


        private struct State : IComparable
        {
            public int Id { get; }
            public T StateAutomaat1 { get; set; }
            public T StateAutomaat2 { get; set; }

            public State(int id, T a1, T a2)
            {
                this.Id = id;
                this.StateAutomaat1 = a1;
                this.StateAutomaat2 = a2;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is State)) return -1;

                var other = (State)obj;
                return Id.CompareTo(other.Id);
            }

            public Boolean HaseSameStates(T a1, T a2)
            {
                return StateAutomaat1.Equals(a1) && StateAutomaat2.Equals(a2);
            }
        }
        public static Automaat<int> operator &(Automaat<T> a1, Automaat<T> a2)
        {
            return CombineAutomaat(a1, a2, (b, b1) => b && b1);
        }

        public static Automaat<int> operator |(Automaat<T> a1, Automaat<T> a2)
        {
            return CombineAutomaat(a1, a2, (b, b1) => b || b1);
        }

        public static Automaat<T> operator !(Automaat<T> a)
        {
            var newA = new Automaat<T>(a.GetAlphabet());
            a._startStates.ToList().ForEach(newA.DefineAsStartState);
            a._transitions.ToList().ForEach(newA.AddTransition);
            
            foreach (var state in a._states)
            {
                if (!a.IsFinalState(state))
                {
                    newA.DefineAsFinalState(state);
                }
            }

            return newA;
        }

        private static Automaat<int> CombineAutomaat(Automaat<T> a1, Automaat<T> a2,
            Func<bool, bool, bool> finalstateDefine)
        {
            var combinedAutomaat = BetweenAutomaat(a1, a2);
            var newA = new Automaat<int>(combinedAutomaat.GetAlphabet());

            combinedAutomaat._startStates.ToList().ForEach(state => newA.DefineAsStartState(state.Id));

            combinedAutomaat._transitions.ToList().ForEach(trans => newA.AddTransition(new Transition<int>(trans.FromState.Id, trans.Symbol, trans.ToState.Id)));

            foreach (var state in combinedAutomaat._states)
            {
                if (finalstateDefine(a1.IsFinalState(state.StateAutomaat1), a2.IsFinalState(state.StateAutomaat2)))
                {
                    newA.DefineAsFinalState(state.Id);
                }
            }

            return newA;
        }

        private static Automaat<State> BetweenAutomaat(Automaat<T> a1, Automaat<T> a2)
        {
            var alphabet = new SortedSet<char>(a1.GetAlphabet());
            a2.GetAlphabet().ToList().ForEach(c => alphabet.Add(c));
            var newA = new Automaat<State>(alphabet);

            var stateCounter = 0;
            var beginState = new State(stateCounter, a1._startStates.First(), a2._startStates.First());
            newA.DefineAsStartState(beginState);

            FindNextState(ref newA, ref stateCounter, a1, a2, beginState);

            return newA;
        }

        private static void FindNextState(ref Automaat<State> a, ref int counter, Automaat<T> a1, Automaat<T> a2, State s)
        {
            foreach (var c in a.GetAlphabet())
            {
                var a1State = a1.GetTransitions(s.StateAutomaat1, c).First().ToState;
                var a2State = a2.GetTransitions(s.StateAutomaat2, c).First().ToState;
                var newState = default(State);
                var stateExsists = false;

                foreach (var state in a._states)
                {
                    if (!state.HaseSameStates(a1State, a2State)) continue;
                    newState = state;
                    stateExsists = true;
                    break;
                }

                if (!stateExsists)
                {
                    newState = new State(++counter, a1State, a2State);
                    a.AddTransition(new Transition<State>(s, c, newState));
                    FindNextState(ref a, ref counter, a1, a2, newState);
                }
                else
                {
                    a.AddTransition(new Transition<State>(s, c, newState));
                }
                
            }

        }
    }
}
