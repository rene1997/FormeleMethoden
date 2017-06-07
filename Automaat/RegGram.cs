using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    class RegGram<T> where T : IComparable
    {
        public readonly SortedSet<T> Symbols;
        public SortedSet<char> Alphabet { get; private set; }
        public readonly SortedSet<PRule<T>> ProductionRules;
        public T StartSymbol { get; private set; }

        public RegGram()
        {
            this.Symbols = new SortedSet<T>();
            this.Alphabet = new SortedSet<char>();
            this.ProductionRules = new SortedSet<PRule<T>>();
        }

        public RegGram(SortedSet<char> alphabet) : this()
        {
            AddAlphabet(alphabet);
        }

        public RegGram(char[] alphabet) : this(new SortedSet<char>(alphabet))
        { }

        public void AddAlphabet(SortedSet<char> alphabet)
        {
            this.Alphabet = alphabet;
        }

        public void DefineStartSymbol(T symbol)
        {
            this.Symbols.Add(symbol);
            this.StartSymbol = symbol;
        }

        public void AddProductionRule(PRule<T> rule)
        {
            this.ProductionRules.Add(rule);

            if (rule.FromSymbol != null)
                this.Symbols.Add(rule.FromSymbol);

            if (rule.ToSymbol != null)
                this.Symbols.Add(rule.ToSymbol);
        }
        
        public override string ToString()
        {
            const string sigmaChar = "#";
            var gram = $"G = (N, {sigmaChar}, P, {StartSymbol})";
            var n = "N = {"+string.Join(", ",this.Symbols)+"}";
            var sigma = $"{sigmaChar} = " + "{" +string.Join(", ", this.Alphabet) + "}";

            var list = new List<Tuple<T, List<PRule<T>>>>();
            foreach (var rule in this.ProductionRules)
            {
                var found = false;
                foreach (var tuple in list)
                {
                    if (!tuple.Item1.Equals(rule.FromSymbol)) continue;

                    found = true;
                    tuple.Item2.Add(rule);
                    break;
                }

                if (found) continue;

                var newTuple = new Tuple<T, List<PRule<T>>>(rule.FromSymbol, new List<PRule<T>> {rule});
                list.Add(newTuple);
            }

            var p = "P = {\n";
            foreach (var tuple in list)
            {
                p += $"{tuple.Item1} -> ";
                foreach (var rule in tuple.Item2)
                {
                    p += rule.ToSymbolInString() + " | ";
                }
                p = p.Substring(0, p.Length - 3); 
                p += "\n";
            }
            p += "}";

            //var p = "P = {" + string.Join("\n", this.ProductionRules) + "}";
            
            string[] parts = { n, sigma, p, gram};
            return string.Join("\n", parts);
        }

        public static RegGram<T> NdfaToRegGram(Automaat<T> ndfa)
        {
            var regGram = new RegGram<T>(ndfa.GetAlphabet());
            foreach (var trans in ndfa._transitions)
            {
                regGram.AddProductionRule(new PRule<T>(trans.FromState, trans.Symbol, trans.ToState));
                if (ndfa._finalStates.Contains(trans.ToState))
                {
                    regGram.AddProductionRule(new PRule<T>(trans.FromState, trans.Symbol){ToSymbolIsFinalSymbol = true});
                }
            }

            regGram.DefineStartSymbol(ndfa._startStates.First());


            return regGram;
        }
    }
}
