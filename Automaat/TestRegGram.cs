using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    class TestRegGram
    {
        public TestRegGram()
        {
            //Als N = { S, A }
            //    $ = { a, b }
            //P = { S -> bS, S -> aA, A -> bA, A -> b }
            //Dan is G = (N, $, P, S) een grammatica.

            char[] alphabet = { 'a', 'b' };
            var RegGram = new RegGram<string>(alphabet);
            
            RegGram.DefineStartSymbol("S");

            RegGram.AddProductionRule(new PRule<string>("S", alphabet[1], "S"));
            RegGram.AddProductionRule(new PRule<string>("S", alphabet[0], "A"));

            RegGram.AddProductionRule(new PRule<string>("A", alphabet[1], "A"));
            RegGram.AddProductionRule(new PRule<string>("A", alphabet[1]));

            Console.WriteLine(RegGram.ToString());
        }
    }
}
