using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class Thompson
    {
        public static Automaat<string> CreateAutomaat(RegExp reg)
        {
            var automaat = new Automaat<string>();
            int stateCounter = 1, leftState = 0, rightState = 1;

            automaat.DefineAsStartState($"{leftState}");
            automaat.DefineAsFinalState($"{rightState}");

            ModifyAutomaat(reg, ref automaat, ref stateCounter, leftState, rightState);

            return automaat;
        }

        private static void ModifyAutomaat(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            switch (reg._operator)
            {
                case RegExp.Operator.PLUS:
                    Regel5(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegExp.Operator.STAR:
                    Regel6(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegExp.Operator.OR:
                    Regel4(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegExp.Operator.DOT:
                    Regel3(reg, ref a, ref c, leftState, rightState);
                    break;
                case RegExp.Operator.ONE:
                    Regel1En2(reg, ref a, ref c, leftState, rightState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void Regel1En2(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            var symbol = reg.terminals.First();
            a._symbols.Add(symbol);
            a.AddTransition(new Transition<string>($"{leftState}",symbol,$"{rightState}"));
        }

        public static void Regel3(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            var newState = c + 1;
            c = newState;
            ModifyAutomaat(reg.left,ref a, ref c, leftState, newState);
            ModifyAutomaat(reg.right, ref a, ref c, newState, rightState);
        }

        public static void Regel4(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            var newLeftState = c + 1;
            var newRightState = newLeftState + 1;
            c = newRightState;
            a.AddTransition(new Transition<string>($"{leftState}",Transition<String>.Epsilon,$"{newLeftState}"));
            a.AddTransition(new Transition<string>($"{newRightState}", Transition<String>.Epsilon, $"{rightState}"));
            ModifyAutomaat(reg.left,ref a, ref c,newLeftState, newRightState);

            newLeftState = c + 1;
            newRightState = newLeftState + 1;
            c = newRightState;
            a.AddTransition(new Transition<string>($"{leftState}", Transition<String>.Epsilon, $"{newLeftState}"));
            a.AddTransition(new Transition<string>($"{newRightState}", Transition<String>.Epsilon, $"{rightState}"));
            ModifyAutomaat(reg.right, ref a, ref c, newLeftState, newRightState);
        }

        public static void Regel5(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            var newLeftState = c + 1;
            var newRightState = newLeftState + 1;
            c = newRightState;

            a.AddTransition(new Transition<string>($"{leftState}",Transition<string>.Epsilon,$"{newLeftState}"));
            a.AddTransition(new Transition<string>($"{newRightState}",Transition<string>.Epsilon,$"{rightState}"));
            a.AddTransition(new Transition<string>($"{newRightState}", Transition<string>.Epsilon, $"{newLeftState}"));
            ModifyAutomaat(reg.left, ref a, ref c, newLeftState, newRightState);
        }

        public static void Regel6(RegExp reg, ref Automaat<string> a, ref int c, int leftState, int rightState)
        {
            var newLeftState = c + 1;
            var newRightState = newLeftState + 1;
            c = newRightState;

            a.AddTransition(new Transition<string>($"{leftState}", Transition<string>.Epsilon, $"{rightState}"));
            a.AddTransition(new Transition<string>($"{leftState}", Transition<string>.Epsilon,$"{newLeftState}"));
            a.AddTransition(new Transition<string>($"{newRightState}", Transition<string>.Epsilon, $"{rightState}"));
            a.AddTransition(new Transition<string>($"{newRightState}", Transition<string>.Epsilon, $"{newLeftState}"));
            ModifyAutomaat(reg.left,ref a,ref c,newLeftState,newRightState);
        }
    }
}
