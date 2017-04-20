using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public class TestAutomaat
    {
        public static Automaat<string> getExampleSlide8Lesson2()            
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<string> m = new Automaat<string>(alphabet);

            m.addTransition(new Transition<string>("q0", 'a', "q1"));
            m.addTransition(new Transition<string>("q0", 'b', "q4"));

            m.addTransition(new Transition<string>("q1", 'a', "q4"));
            m.addTransition(new Transition<string>("q1", 'b', "q2"));

            m.addTransition(new Transition<string>("q2", 'a', "q3"));
            m.addTransition(new Transition<string>("q2", 'b', "q4"));

            m.addTransition(new Transition<string>("q3", 'a', "q1"));
            m.addTransition(new Transition<string>("q3", 'b', "q2"));

            // the error state, loops for a and b:
            m.addTransition(new Transition<string>("q4", 'a'));
            m.addTransition(new Transition<string>("q4", 'b'));

            // only on start state in a dfa:
            m.defineAsStartState("q0");

            // two final states:
            m.defineAsFinalState("q2");
            m.defineAsFinalState("q3");

            return m;
        }
    }
}
