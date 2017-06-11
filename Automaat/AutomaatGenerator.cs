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
            BEGINT_MET,
            BEVAT,
            EINDIGT_OP
        }
        public static Automaat<int> GenerateAutomaat(string symbols, char[] alphabet, AutomaatType type)
        {
            switch (type)
            {
                case AutomaatType.BEGINT_MET:
                    return BegintMet(symbols, alphabet);
                case AutomaatType.BEVAT:
                    return Bevat(symbols, alphabet);
                case AutomaatType.EINDIGT_OP:
                    return EindigtOp(symbols, alphabet);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static Automaat<int> BegintMet(string symbols, char[] alphabet)
        {
            var a = new Automaat<int>(alphabet);
            var stateCounter = 0;

            var wrongState = stateCounter++;
            var fromState = stateCounter;
            
            a.DefineAsStartState(fromState);
            foreach (var s in symbols.ToCharArray())
            {
                stateCounter++;
                a.AddTransition(new Transition<int>(fromState, s, stateCounter));

                foreach (var letter in a.GetAlphabet())
                {
                    if (s == letter) continue;

                    a.AddTransition(new Transition<int>(fromState, letter, wrongState));
                }

                fromState = stateCounter;
            }

            // on this point, the statecounter is the final state
            foreach (var s in a.GetAlphabet())
            {
                a.AddTransition(new Transition<int>(wrongState, s));
                a.AddTransition(new Transition<int>(stateCounter, s));
            }
            a.DefineAsFinalState(stateCounter);
            
            return a;
        }

        private static Automaat<int> Bevat(string rulesymbols, char[] alphabet)
        {
            var a = new Automaat<int>(alphabet);
            var stateCounter = 0;
            var startState = ++stateCounter;
            
            var fromState = startState;

            a.DefineAsStartState(fromState);

            var word = "";

            foreach (var s in rulesymbols.ToCharArray())
            {
                stateCounter++;
                word += s;
                foreach (var letter in a.GetAlphabet())
                {
                    if (s == letter)
                    {
                        a.AddTransition(new Transition<int>(fromState, s, stateCounter));
                    }
                    else
                    {
                        int i;
                        for (i = 0; i < word.Length; i++)
                        {
                            if (word[i] != letter) break;
                        }
                        a.AddTransition(new Transition<int>(fromState, letter, i + startState));
                    }
                }
                fromState = stateCounter;
            }

            foreach (var s in a.GetAlphabet())
            {
                a.AddTransition(new Transition<int>(stateCounter, s));
            }

            a.DefineAsFinalState(stateCounter);

            return a;
        }

        private static Automaat<int> EindigtOp(string rulesymbols, char[] alphabet)
        {
            var a = new Automaat<int>(alphabet);
            var stateCounter = 0;
            var startState = ++stateCounter;

            var fromState = startState;

            a.DefineAsStartState(fromState);

            var word = "";

            foreach (var s in rulesymbols.ToCharArray())
            {
                stateCounter++;
                word += s;
                foreach (var letter in a.GetAlphabet())
                {
                    if (s == letter)
                    {
                        a.AddTransition(new Transition<int>(fromState, s, stateCounter));
                    }
                    else
                    {
                        int i;
                        for (i = 0; i < word.Length; i++)
                        {
                            if (word[i] != letter) break;
                        }
                        a.AddTransition(new Transition<int>(fromState, letter, i + startState));
                    }
                }
                fromState = stateCounter;
            }
            
            foreach (var letter in a.GetAlphabet())
            {
                int i;
                for (i = 0; i < rulesymbols.Length; i++)
                {
                    if (word[i] != letter) break;
                }
                a.AddTransition(new Transition<int>(fromState, letter, i + startState));
            }

            a.DefineAsFinalState(stateCounter);

            return a;
        }
    }
}
