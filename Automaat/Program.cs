using System;
using System.Collections.Generic;

namespace Automaat
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            //testTranstion();
            //Automaat<string> a1 = TestAutomaat.GetExampleSlide8Lesson2();
            //Automaat<string> a2 = TestAutomaat.GetExampleSlide14Lesson2();

            //a1.printTransitions();
            //Console.WriteLine("is automaat 1 a DFA: " + a1.IsDfa());

            //a2.printTransitions();
            //Console.WriteLine("is automaat 2 a DFA: " + a2.IsDfa());

            //PracL1ERepresentatie1();
            //PracL1Representatie2();
            //PracL1Representatie3();
            //PracL1Representatie3();
            //PracL1Representatie4();
            //Ndfa();
            //new TestRegExp().testLanguage();
            testEpsilonNDFA();
            //TestThompson.TestRegToAutomaat();
            //            testEpsilonNDFA();
            //TestThompson.TestRegToAutomaat();
            
            Console.ReadLine();
        }
                
        
        static void TestTranstion()
        {
            Console.WriteLine("Testing equals method for transition");
            Console.WriteLine("Building transitions");
            Transition<string> t1 = new Transition<string>("q1", 'a', "q2");
            Transition<string> t2 = new Transition<string>("q1", 'a', "q2");
            Transition<string> t3 = new Transition<string>("q1", 'a', "q3");
            Transition<string> t4 = new Transition<string>("q2", 'a', "q1");
            Transition<string> t5 = new Transition<string>("q1", 'b', "q2");

            Console.WriteLine("Transition 1: " + t1);
            Console.WriteLine("Transition 2: " + t2);
            Console.WriteLine("Transition 3: " + t3);
            Console.WriteLine("Transition 4: " + t4);
            Console.WriteLine("Transition 5: " + t5);

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

        static void TestingAutomaat(string automaat_name, Automaat<string> a, List<Tuple<string, bool>> testWords)
        {
            Console.WriteLine($"Testing automaat class, {automaat_name}");
            Console.WriteLine("Automaat is a DFA: " + a.IsDfa());            
            
            foreach(Tuple<string, bool> word in testWords)
            {
                var geaccepteerd = a.Accepteer(word.Item1);
                Console.WriteLine($"Word: is {word.Item1} accepted?, expected: {word.Item2}, result: {geaccepteerd}");
                if (word.Item2 != geaccepteerd) Console.WriteLine("Niet verwachte resultaat");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
        }

        static void PracL1ERepresentatie1()
        {
            /*begint met abb of eindigt op baab*/
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", 'a', "2"));
            m.AddTransition(new Transition<string>("1", 'b', "5"));

            m.AddTransition(new Transition<string>("2", 'a', "9"));
            m.AddTransition(new Transition<string>("2", 'b', "3"));

            m.AddTransition(new Transition<string>("3", 'a', "6"));
            m.AddTransition(new Transition<string>("3", 'b', "4"));

            m.AddTransition(new Transition<string>("4", 'a', "4"));
            m.AddTransition(new Transition<string>("4", 'b', "4"));

            m.AddTransition(new Transition<string>("5", 'a', "6"));
            m.AddTransition(new Transition<string>("5", 'b', "5"));

            m.AddTransition(new Transition<string>("6", 'a', "7"));
            m.AddTransition(new Transition<string>("6", 'b', "5"));

            m.AddTransition(new Transition<string>("7", 'a', "9"));
            m.AddTransition(new Transition<string>("7", 'b', "8"));

            m.AddTransition(new Transition<string>("8", 'a', "6"));
            m.AddTransition(new Transition<string>("8", 'b', "5"));

            m.AddTransition(new Transition<string>("9", 'a', "9"));
            m.AddTransition(new Transition<string>("9", 'b', "5"));


            // only on start state in a dfa:
            m.DefineAsStartState("1");

            // two final states:
            m.DefineAsFinalState("4");
            m.DefineAsFinalState("8");

            List<Tuple<string, bool>> testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("abb", true));
            testWords.Add(new Tuple<string, bool>("baab", true));
            testWords.Add(new Tuple<string, bool>("aabb", false));
            testWords.Add(new Tuple<string, bool>("ab", false));
            testWords.Add(new Tuple<string, bool>("baa", false));
            testWords.Add(new Tuple<string, bool>("aabbabaaabaab", true));
            testWords.Add(new Tuple<string, bool>("ababaabaa", false));
            testWords.Add(new Tuple<string, bool>("abba", true));
            TestingAutomaat("Begint met abb of eindigt op baab",m, testWords);

        }

        static void PracL1Representatie2()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("1", alphabet[1], "3"));

            m.AddTransition(new Transition<string>("2", alphabet[0], "1"));
            m.AddTransition(new Transition<string>("2", alphabet[1], "5"));

            m.AddTransition(new Transition<string>("3", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("3", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("4", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("4", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("5", alphabet[0], "1"));
            m.AddTransition(new Transition<string>("5", alphabet[1], "4"));

            // only on start state in a dfa:
            m.DefineAsStartState("1");

            // two final states:
            m.DefineAsFinalState("2");
            m.DefineAsFinalState("4");
            m.DefineAsFinalState("5");

            List<Tuple<string, bool>> testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("a", true));
            testWords.Add(new Tuple<string, bool>("bb", true));
            testWords.Add(new Tuple<string, bool>("abb", true));
            testWords.Add(new Tuple<string, bool>("aabb", true));
            testWords.Add(new Tuple<string, bool>("baa", false));
            testWords.Add(new Tuple<string, bool>("aabbabaaabab", true));
            testWords.Add(new Tuple<string, bool>("ababaabaa", false));
            testWords.Add(new Tuple<string, bool>("abba", true));
            testWords.Add(new Tuple<string, bool>("babab", false));
            testWords.Add(new Tuple<string, bool>("ababa", true));
            TestingAutomaat("Bevat een even aantal b’s of bevat een oneven aantal a’s",m, testWords);
            var a = m.GeefTaal(2);
            Console.WriteLine(a.Count);
        }

        static void PracL1Representatie3()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", alphabet[0], "3"));
            m.AddTransition(new Transition<string>("1", alphabet[1], "2"));

            m.AddTransition(new Transition<string>("2", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("2", alphabet[1], "1"));

            m.AddTransition(new Transition<string>("3", alphabet[0], "4"));
            m.AddTransition(new Transition<string>("3", alphabet[1], "2"));

            m.AddTransition(new Transition<string>("4", alphabet[0], "3"));
            m.AddTransition(new Transition<string>("4", alphabet[1], "5"));

            m.AddTransition(new Transition<string>("5", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("5", alphabet[1], "1"));

            // only on start state in a dfa:
            m.DefineAsStartState("1");

            // two final states:
            m.DefineAsFinalState("5");

            List<Tuple<string, bool>> testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("aab", true));
            testWords.Add(new Tuple<string, bool>("bbaab", true));
            testWords.Add(new Tuple<string, bool>("bbbaab", false));
            testWords.Add(new Tuple<string, bool>("aabaabaab", true));
            testWords.Add(new Tuple<string, bool>("bbaabb", false));
            testWords.Add(new Tuple<string, bool>("aabaab", false));
            testWords.Add(new Tuple<string, bool>("ababaabaa", false));
            TestingAutomaat("Bevat een even aantal b’s en eindigt op aab", m, testWords);
        }

        static void PracL1Representatie4()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("1", alphabet[1], "8"));

            m.AddTransition(new Transition<string>("2", alphabet[0], "8"));
            m.AddTransition(new Transition<string>("2", alphabet[1], "3"));

            m.AddTransition(new Transition<string>("3", alphabet[0], "8"));
            m.AddTransition(new Transition<string>("3", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("4", alphabet[0], "5"));
            m.AddTransition(new Transition<string>("4", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("5", alphabet[0], "6"));
            m.AddTransition(new Transition<string>("5", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("6", alphabet[0], "4"));
            m.AddTransition(new Transition<string>("6", alphabet[1], "7"));

            m.AddTransition(new Transition<string>("7", alphabet[0], "7"));
            m.AddTransition(new Transition<string>("7", alphabet[1], "7"));

            m.AddTransition(new Transition<string>("8", alphabet[0], "8"));
            m.AddTransition(new Transition<string>("8", alphabet[1], "8"));

            // only one start state in a dfa:
            m.DefineAsStartState("1");

            // two final states:
            m.DefineAsFinalState("7");

            List<Tuple<string, bool>> testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("abbaabababab", true));
            testWords.Add(new Tuple<string, bool>("abbaab", true));
            testWords.Add(new Tuple<string, bool>("abb", false));
            testWords.Add(new Tuple<string, bool>("abaab", false));
            testWords.Add(new Tuple<string, bool>("ababaaba", false));
            TestingAutomaat("Begint met abb en bevat baab",m, testWords);
        }

        static void Ndfa()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("1", alphabet[0], "3"));
            m.AddTransition(new Transition<string>("1", alphabet[1], "1"));

            m.AddTransition(new Transition<string>("2", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("2", alphabet[1], "2"));

            m.AddTransition(new Transition<string>("3", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("3", alphabet[1], "4"));

            m.AddTransition(new Transition<string>("4", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("4", alphabet[1], "2"));

            // only on start state in a dfa:
            m.DefineAsStartState("1");

            // two final states:
            m.DefineAsFinalState("4");

            var testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("ab", true));
            testWords.Add(new Tuple<string, bool>("ba", false));
            testWords.Add(new Tuple<string, bool>("aa", false));
            testWords.Add(new Tuple<string, bool>("bbab", true));

            TestingAutomaat("testing code for ndfa", m, testWords);
        }

        static void testEpsilonNDFA()
        {
            char[] alphabet = { 'a', 'b' };
            var m = new Automaat<string>(alphabet);

            m.AddTransition(new Transition<string>("1", alphabet[0], "1"));
            m.AddTransition(new Transition<string>("1", alphabet[0], "2"));
            m.AddTransition(new Transition<string>("1", Transition<string>.Epsilon, "3"));

            m.AddTransition(new Transition<string>("2", alphabet[1], "4"));
            m.AddTransition(new Transition<string>("3", alphabet[1], "5"));

            m.DefineAsStartState("1");
            m.DefineAsFinalState("5");

            var testWords = new List<Tuple<string, bool>>();
            testWords.Add(new Tuple<string, bool>("b", true));
            testWords.Add(new Tuple<string, bool>("ab", true));
            testWords.Add(new Tuple<string, bool>("a", false));
            testWords.Add(new Tuple<string, bool>("bba", true));
            TestingAutomaat("Testing for epsilon transitions", m, testWords);

            NDFAToDFA<string>.MakeDFA(m);
        }
    }
}
