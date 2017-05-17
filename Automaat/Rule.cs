using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public abstract class Rule<T> where T : IComparable
    {
        public RegExp.Operator _operator;       
        
        public Rule(RegExp.Operator _operator)
        {
            this._operator = _operator;
        }

        public abstract void Use(RegExp reg, ref Automaat<T> automaat);
    }

    public class Regel1And2<T> : Rule<T> where T : IComparable
    {
        public Regel1And2( ) : base(RegExp.Operator.ONE)
        {
        }

        public override void Use(RegExp reg, ref Automaat<T> automaat)
        {
            
        }
    }

    public class Regel3<T> : Rule<T> where T : IComparable
    {
        public Regel3() : base(RegExp.Operator.DOT)
        {
        }

        public override void Use(RegExp reg, ref Automaat<T> automaat)
        {
            throw new NotImplementedException();
        }
    }

    public class Regel4<T> : Rule<T> where T : IComparable
    {
        public Regel4() : base(RegExp.Operator.OR)
        {
        }

        public override void Use(RegExp reg, ref Automaat<T> automaat)
        {
            throw new NotImplementedException();
        }
    }

    public class Regel5<T> : Rule<T> where T : IComparable
    {
        public Regel5() : base(RegExp.Operator.PLUS)
        {
        }

        public override void Use(RegExp reg, ref Automaat<T> automaat)
        {
            throw new NotImplementedException();
        }
    }

    public class Regel6<T> : Rule<T> where T : IComparable
    {
        public Regel6() : base(RegExp.Operator.STAR)
        {
        }

        public override void Use(RegExp reg, ref Automaat<T> automaat)
        {
            throw new NotImplementedException();
        }
    }
}
