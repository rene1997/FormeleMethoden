using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    class AutomaatGenerator
    {
        public enum AutomaatType
        {
            BEGINT_MET
        }
        public static Automaat<int> GenerateAutomaat(string symbols, AutomaatType type)
        {
            switch (type)
            {
                case AutomaatType.BEGINT_MET:
                    return BegintMet(symbols);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static Automaat<int> BegintMet(string symbols)
        {
            var alphabet = symbols.ToCharArray();
            var a = new Automaat<int>(alphabet);
            var stateCounter = 0;

            var wrongState = stateCounter++;
            var fromState = stateCounter;
            
            a.DefineAsStartState(fromState);
            foreach (var s in alphabet)
            {
                stateCounter++;
                a.AddTransition(new Transition<int>(fromState, s, stateCounter));

                foreach (var sa in a.GetAlphabet())
                {
                    if (s == sa) continue;

                    a.AddTransition(new Transition<int>(fromState, sa, wrongState));
                }

                fromState = stateCounter;
            }
            a.DefineAsFinalState(stateCounter);
            return a;
        }
    }
}
