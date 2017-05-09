﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class TestRegExp
    {
        private RegExp expr1, expr2, expr3, expr4, expr5, a, b, all;

        public TestRegExp()
        {
            a = new RegExp("a");
            b = new RegExp("b");

            // expr1: "baa"
            expr1 = new RegExp("baa");
            // expr2: "bb"
            expr2 = new RegExp("bb");
            // expr3: "baa | baa"
            expr3 = expr1.or(expr2);

            // all: "(a|b)*"
            all = (a.or(b)).star();

            // expr4: "(baa | baa)+"
            expr4 = expr3.plus();
            // expr5: "(baa | baa)+ (a|b)*"
            expr5 = expr4.dot(all);
        }

        public void testLanguage()
        {
           Console.WriteLine("taal van (baa):\n" + printSet(expr1.getLanguage(5)));
           Console.WriteLine("taal van (bb):\n" + printSet(expr2.getLanguage(5)));
           Console.WriteLine("taal van (baa | bb):\n" + printSet(expr3.getLanguage(5)));

           Console.WriteLine("taal van (a|b)*:\n" + printSet(all.getLanguage(5)));
           Console.WriteLine("taal van (baa | bb)+:\n" + printSet(expr4.getLanguage(5)));
           Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n" + printSet(expr5.getLanguage(6)));
        }

        private String printSet<T>(SortedSet<T> s)
        {
            String s1 = "";
            s.ToList().ForEach(x => s1 += x +  ",");
            return s1;
        }
    }

}
