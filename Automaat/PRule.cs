using System;
using System.Collections.Generic;

namespace Automaat
{
    class PRule<T> : IComparable<PRule<T>> where T : IComparable
    {
        //production rule
        public readonly T FromSymbol;
        public readonly char Letter;
        public readonly T ToSymbol;
        public bool ToSymbolIsFinalSymbol { get; set; }
        
        public PRule(T fromSymbol, char letter, T toSymbol)
        {
            this.FromSymbol = fromSymbol;
            this.Letter = letter;
            this.ToSymbol = toSymbol;
            this.ToSymbolIsFinalSymbol = false;
        }

        public PRule(T fromSymbol, char letter)
        {
            this.FromSymbol = fromSymbol;
            this.Letter = letter;
            this.ToSymbol = default(T);
        }

        public string ToSymbolInString()
        {
            return this.ToSymbolIsFinalSymbol ? $"{this.Letter}" : $"{this.Letter}{this.ToSymbol}";
        }

        public override string ToString()
        {
            return $"{FromSymbol} -> {this.ToSymbolInString()}";
        }

        public int CompareTo(PRule<T> other)
        {
            var fromCmp = FromSymbol.CompareTo(other.FromSymbol);
            var symbolCmp = Letter.CompareTo(other.Letter);
            var toCmp = 1;

            if (ToSymbol != null)
                toCmp = ToSymbol.CompareTo(other.ToSymbol);

            return fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp);
        }
    }
}