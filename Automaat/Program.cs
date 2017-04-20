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
            testTranstion();


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
    }
}
