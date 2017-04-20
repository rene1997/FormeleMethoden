using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class Transition<T> : IComparable<Transition<T>> where T : IComparable
    {
        public const char EPSILON = '$';

        public T fromState { get; }
        public char symbol { get; }
        public T toState { get; }

        public Transition(T fromOrTo, char s) : this(fromOrTo, s, fromOrTo)
        { }

        public Transition(T from, T to) : this(from, EPSILON, to)
        { }

        public Transition(T from, char s, T to)
        {
            this.fromState = from;
            this.symbol = s;
            this.toState = to;
        }
        
        public override bool Equals(Object other)
        {
            if (other == null) return false;

            if (other is Transition<T>)
            {
                Transition<T> oT = (Transition<T>)other;
                return
                    fromState.CompareTo(oT.fromState) == 0 &&
                    toState.CompareTo(oT.toState) == 0 &&
                    symbol == oT.symbol;
            }

            return false;
        }

        public int CompareTo(Transition<T> other)
        {
            int fromCmp = fromState.CompareTo(other.fromState);
            int symbolCmp = symbol.CompareTo(other.symbol);
            int toCmp = toState.CompareTo(other.toState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public override string ToString()
        {
            return $"({this.fromState}, {this.symbol}) --> {this.toState}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
