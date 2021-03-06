﻿using System;
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
            //(a|b)*
            var regex = new RegExp("a");
            regex = regex.or(new RegExp("b"));
            regex = regex.star();
            TestRegex(regex);

            //((ab))+
            regex = new RegExp("a");
            regex = regex.dot(new RegExp("b"));
            regex = regex.plus();
            TestRegex(regex);

            //((a|b)|(a|d))
            RegExp rA, rB, rC, rD, reg;
            rA = new RegExp("a");
            rB = new RegExp("b");
            rC = new RegExp("c");
            rD = new RegExp("d");

            reg = rA.or(rB);
            reg = reg.or(rC.or(rD));
            TestRegex(reg);
        }

        private static void TestRegex(RegExp reg)
        {
            Console.WriteLine($"\n{reg.ToString()}");
            var automaat = Thompson.CreateAutomaat(reg);
            automaat.PrintTransitions();
            Console.WriteLine("alphabet:");
            foreach (var s in automaat.GetAlphabet())
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\ntalen die regex accepteert");
            foreach (var s in reg.getLanguage(3))
            {
                Console.WriteLine($"Taal:{s}, wordt geaccepteerd:{automaat.Accepteer(s)}\n");
            }
        }
    }
}
