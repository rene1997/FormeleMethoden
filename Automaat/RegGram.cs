using System;
using System.Collections.Generic;
using System.Linq;
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

        public RegGram(char[] alphabet)
        {
            this.Symbols = new SortedSet<T>();
            AddAlphabet(new SortedSet<char>(alphabet));
            this.ProductionRules = new SortedSet<PRule<T>>();
        }

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
            var p = "P = {" + string.Join(", ", this.ProductionRules) + "}";

            string[] parts = { n, sigma, p, gram};
            return string.Join("\n", parts);
        }
    }
}
