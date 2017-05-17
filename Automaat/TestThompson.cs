using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class TestThompson
    {
        public static void TestRegToAutomaat()
        {
            //regex == (a|b)*
            var regex = new RegExp("a");
            regex = regex.or(new RegExp("b"));
            regex = regex.star();
            
            var automaat = Thompson<string>.CreateAutomaat(regex);
            automaat.PrintTransitions();
            var alphabet = automaat.GetAlphabet();
        }
    }
}
