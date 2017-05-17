using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class Thompson<T> where T : IComparable
    {
        private static List<Rule<T>> rules = new List<Rule<T>>()
        {
            new Regel1And2<T>(),
            new Regel3<T>(),
            new Regel4<T>(),
            new Regel5<T>(),
            new Regel6<T>()
        };
        public static Automaat<T> CreateAutomaat(RegExp reg)
        {
            char[] alphabet = reg.GetAlphabet().ToArray();
            var automaat = new Automaat<T>(alphabet);
            HandleOperator(reg, ref automaat);
            foreach(var r in rules)
            {
                if (r._operator == reg._operator)
                {
                    //r.Use(reg, ref automaat);
                }
            }
            return automaat;
        }

        private static void HandleOperator(RegExp reg, ref Automaat<T> automaat)
        {
            foreach(var f in rules)
            {
                if (f._operator == reg._operator)
                {
                    f.Use(reg, ref automaat);
                }
            }
        }
    }
}
