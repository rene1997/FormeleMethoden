using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class Transition<T> : IComparable<Transition<T>> where T : IComparable
    {
        public const char Epsilon = '$';

        public T FromState { get; }
        public char Symbol { get; }
        public T ToState { get; }

        public Transition(T fromOrTo, char s) : this(fromOrTo, s, fromOrTo)
        { }

        public Transition(T from, T to) : this(from, Epsilon, to)
        { }

        public Transition(T from, char s, T to)
        {
            this.FromState = from;
            this.Symbol = s;
            this.ToState = to;
        }
        
        public override bool Equals(Object other)
        {
            if (other == null) return false;

            if (other is Transition<T>)
            {
                Transition<T> oT = (Transition<T>)other;
                return
                    FromState.CompareTo(oT.FromState) == 0 &&
                    ToState.CompareTo(oT.ToState) == 0 &&
                    Symbol == oT.Symbol;
            }

            return false;
        }

        public int CompareTo(Transition<T> other)
        {
            int fromCmp = FromState.CompareTo(other.FromState);
            int symbolCmp = Symbol.CompareTo(other.Symbol);
            int toCmp = ToState.CompareTo(other.ToState);

            return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
        }

        public override string ToString()
        {
            return $"({this.FromState}, {this.Symbol}) --> {this.ToState}";
        }

        public bool IsEpsilonTransition()
        {
            return Symbol == Epsilon;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
