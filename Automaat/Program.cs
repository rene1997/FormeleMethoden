using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            //testTranstion();
            Automaat<string> a1 = TestAutomaat.getExampleSlide8Lesson2();
            Automaat<string> a2 = TestAutomaat.getExampleSlide14Lesson2();

            a1.printTransitions();
            Console.WriteLine("is automaat 1 a DFA: " + a1.isDFA());

            a2.printTransitions();
            Console.WriteLine("is automaat 2 a DFA: " + a2.isDFA());

            pracL1Rep1();

            Console.ReadLine();
        }

        static void testTranstion()
        {
            Console.WriteLine("Testing equals method for transition");
            Console.WriteLine("Building transitions");
            Transition<string> t1 = new Transition<string>("q1", 'a', "q2");
            Transition<string> t2 = new Transition<string>("q1", 'a', "q2");
            Transition<string> t3 = new Transition<string>("q1", 'a', "q3");
            Transition<string> t4 = new Transition<string>("q2", 'a', "q1");
            Transition<string> t5 = new Transition<string>("q1", 'b', "q2");

            Console.WriteLine("Transition 1: " + t1.ToString());
            Console.WriteLine("Transition 2: " + t2.ToString());
            Console.WriteLine("Transition 3: " + t3.ToString());
            Console.WriteLine("Transition 4: " + t4.ToString());
            Console.WriteLine("Transition 5: " + t5.ToString());

            Console.WriteLine("Getting results");
            bool test1,test2,test3,test4;
            test1 = t1.Equals(t2);
            test2 = t1.Equals(t3);
            test3 = t1.Equals(t4);
            test4 = t1.Equals(t5);
            Console.WriteLine("Results:");

            Console.WriteLine("Test 1, equal parameters");
            Console.WriteLine("True: "+test1);
            Console.WriteLine("Test 2, different toState");
            Console.WriteLine("False: "+test2);
            Console.WriteLine("Test 3, different fromState");
            Console.WriteLine("False: "+test3);
            Console.WriteLine("Test 4, different symbol");
            Console.WriteLine("False: "+test4);
        }

        static void testAutomaat(Automaat<string> a, List<Tuple<string, bool>> testWords)
        {
            Console.WriteLine("Testing automaat class");
            Console.WriteLine("Automaat is a DFA: " + a.isDFA());            
            
            foreach(Tuple<string, bool> word in testWords)
            {
                Console.WriteLine($"Word: {word.Item1} is accepted, expacted: {word.Item2}, result: {a.accepteer(word.Item1)}");
            }
        }

        static void pracL1Rep1()
        {
            /*begint met abb of eindigt op baab*/
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.addTransition(new Transition<string>("1", 'a', "2"));
            m.addTransition(new Transition<string>("1", 'b', "5"));

            m.addTransition(new Transition<string>("2", 'a', "9"));
            m.addTransition(new Transition<string>("2", 'b', "3"));

            m.addTransition(new Transition<string>("3", 'a', "6"));
            m.addTransition(new Transition<string>("3", 'b', "4"));

            m.addTransition(new Transition<string>("4", 'a', "4"));
            m.addTransition(new Transition<string>("4", 'b', "4"));

            m.addTransition(new Transition<string>("5", 'a', "6"));
            m.addTransition(new Transition<string>("5", 'b', "5"));

            m.addTransition(new Transition<string>("6", 'a', "7"));
            m.addTransition(new Transition<string>("6", 'b', "5"));

            m.addTransition(new Transition<string>("7", 'a', "9"));
            m.addTransition(new Transition<string>("7", 'b', "8"));

            m.addTransition(new Transition<string>("8", 'a', "6"));
            m.addTransition(new Transition<string>("8", 'b', "5"));

            m.addTransition(new Transition<string>("9", 'a', "9"));
            m.addTransition(new Transition<string>("9", 'b', "5"));


            // only on start state in a dfa:
            m.defineAsStartState("1");

            // two final states:
            m.defineAsFinalState("4");
            m.defineAsFinalState("8");

            List<Tuple<string, bool>> testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("abb", true));
            testWords.Add(new Tuple<string, bool>("baab", true));
            testWords.Add(new Tuple<string, bool>("aabb", false));
            testWords.Add(new Tuple<string, bool>("ab", false));
            testWords.Add(new Tuple<string, bool>("baa", false));
            testWords.Add(new Tuple<string, bool>("aabbabaaabaab", true));
            testWords.Add(new Tuple<string, bool>("ababaabaa", false));
            testWords.Add(new Tuple<string, bool>("abba", true));
            testAutomaat(m, testWords);
        }
    }
}
