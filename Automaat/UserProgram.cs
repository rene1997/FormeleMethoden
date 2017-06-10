using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    public struct AutomateStruct
    {
        public Automaat<int> automaat;
        public string text;
    }

    public class UserProgram
    {

        

        public static int GetInput(int max)
        {
            int input = 0;
            var sInput = Console.ReadLine();
            int.TryParse(sInput, out input);
            if (input >= max || input < 0) return 0;
            return input;
        }

        String[] Hoofdmenu = new string[] {
            "regex => ndfa",
            "ndfa => dfa",
            "ndfa => grammatica",
            "dfa => minimalisatie",
            "thompson",
            "gelijkheid reguliere expressies"
        };

        private List<SubMenu> submenus = new List<SubMenu>();

        public UserProgram()
        {
            FillSubmenus();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("welkom, voer een getal in om een functie uit te voeren:");
                int index = 0;
                Hoofdmenu.ToList().ForEach(s => { Console.WriteLine($"{index}) {Hoofdmenu[index]}"); index++; });
                index = GetInput(submenus.Count);
                submenus[index].ShowMenu();
            }
            
        }

        public void FillSubmenus()
        {
            submenus.Add(new RegToNDFA());
            submenus.Add(new NDFAToDFA());
            submenus.Add(new NdfaToGrammatica());
            submenus.Add(new Minimalization());
        }

        private interface SubMenu
        {
            void ShowMenu();
        }

        private class RegToNDFA : SubMenu
        {
            List<Tuple<string, RegExp>> sampleRegex = new List<Tuple<string, RegExp>>();
            public void ShowMenu()
            {
                FillSamples();
                Console.Clear();
                Console.WriteLine("regex => ndfa gekozen \nkies een regex");
                int index = 0;
                sampleRegex.ForEach(r => { Console.WriteLine($"{index}) {r.Item1}");  index++; });
                index = GetInput(sampleRegex.Count);
                if (index >= 0)
                    ShowRegex(index);
            }

            private void FillSamples()
            {
                var a = new RegExp("a");
                var b = new RegExp("b");
                var string1 = "aba(a*|b*)* begint met aba";
                var reg1 = a.dot(b).dot(a).dot(a.or(b).star());
                sampleRegex.Add(new Tuple<string, RegExp>(string1, reg1));
            }

            private void ShowRegex(int index)
            {
                var regex = sampleRegex[index].Item2;
                Console.WriteLine($"regex naar ndfa: " + sampleRegex[index].Item1);
                Console.WriteLine("automaat:");
                var automaat = Thompson.CreateAutomaat(regex);
                automaat.Print();
                bool running = true;
                while (running)
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("kies een volgende actie:");
                    Console.WriteLine("0) toon afbeelding automaat");
                    Console.WriteLine("1) toon afbeelding dfa");
                    Console.WriteLine("2) toon afbeelding geminimaliseerde automaat");
                    Console.WriteLine("3) terug naar hoofdmenu");
                    index = GetInput(4);
                    HandleSubMenu(index, automaat);
                    if (index < 0 || index > 2)
                    {
                        running = false;
                        sampleRegex.Clear();
                    }
                        
                }
            }

            private void HandleSubMenu(int index, Automaat<string> automaat) 
            {
                if (index < 0 || index > 2) return;
                switch (index)
                {
                    case 0:
                        automaat.ViewImage();
                        break;
                    case 1:
                        NdfatoDfa.MakeDfa(automaat).ViewImage();
                        break;
                    case 2:
                        automaat.MinimizeReverse().ViewImage();
                        break;
                }
            }
        }

        private class NDFAToDFA : SubMenu
        {
            private List<AutomateStruct> automates = new List<AutomateStruct>();

            private string[] subsubtext = { "0) NDFA", "1) DFA", "2) Geminimaliseerd", "3) Terug" };

            public void ShowMenu()
            {
                FillSamples();
                Console.Clear();
                Console.WriteLine("NDFA naar DFA gekozen. \n Kies een automaat:");
                int index = 0;
                automates.ForEach(a => { Console.WriteLine($"{index}) {a.text}"); index++; });
                index = GetInput(automates.Count);
                HandleSubInput(automates[index]);
            }

            private void FillSamples()
            {
                char[] alphabet = { 'a', 'b' };
                var m1 = new AutomateStruct();
                m1.text = "eindigt op abb";
                var a1 = new Automaat<int>(alphabet);
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 2));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 4));
                a1.AddTransition(new Transition<int>(2, alphabet[0], 3));
                a1.AddTransition(new Transition<int>(3, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(4, alphabet[1], 5));
                a1.AddTransition(new Transition<int>(5, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(7, alphabet[0], 8));
                a1.AddTransition(new Transition<int>(8, alphabet[1], 9));
                a1.AddTransition(new Transition<int>(9, alphabet[1], 10));
                a1.DefineAsStartState(0);
                a1.DefineAsFinalState(10);
                m1.automaat = a1;
                automates.Add(m1);
            }

            private void HandleSubInput(AutomateStruct automate)
            {
                var dfa = NdfatoDfa.MakeDfa(automate.automaat);
                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"gekozen: {automate.text} \n");
                    subsubtext.ToList().ForEach(s => Console.WriteLine(s));
                    
                    int input = GetInput(subsubtext.Length);

                    switch (input)
                    {
                        case 0:
                            Graphviz.PrintGraph(automate.automaat, "test");
                            break;
                        case 1:
                            Graphviz.PrintGraph(dfa, "test");
                            break;
                        case 2:
                            Graphviz.PrintGraph(dfa.MinimizeReverse(), "test");
                            break;
                        case 3:
                            running = false;
                            automates.Clear();
                            break;
                    }
                }
                
            }
        }

        private class NdfaToGrammatica : SubMenu
        {
            private List<AutomateStruct> automates = new List<AutomateStruct>();
            private string[] subsubtext = { "0) NDFA", "1) Grammatica", "2) Terug"};


            public void ShowMenu()
            {
                FillList();
                Console.Clear();
                Console.WriteLine("Automaat naar grammatica: \nKies een automaat");
                int index = 0;
                automates.ForEach(a =>
                {
                    Console.WriteLine($"{index}) {a.text}");
                    index++;
                });
                index = GetInput(subsubtext.Length);
                HandleSubInput(index);


            }

            private void FillList()
            {
                char[] alphabet = { 'a', 'b' };
                var m1 = new AutomateStruct();
                m1.text = "eindigt op abb";
                var a1 = new Automaat<int>(alphabet);
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 2));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 4));
                a1.AddTransition(new Transition<int>(2, alphabet[0], 3));
                a1.AddTransition(new Transition<int>(3, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(4, alphabet[1], 5));
                a1.AddTransition(new Transition<int>(5, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(7, alphabet[0], 8));
                a1.AddTransition(new Transition<int>(8, alphabet[1], 9));
                a1.AddTransition(new Transition<int>(9, alphabet[1], 10));
                a1.DefineAsStartState(0);
                a1.DefineAsFinalState(10);
                m1.automaat = a1;
                automates.Add(m1);
            }

            private void HandleSubInput(int index)
            {
                var running = true;
                var automaat = automates[index].automaat;
                while(running)
                {
                    Console.Clear();
                    Console.WriteLine($"Gekozen {automates[index].text}");
                    subsubtext.ToList().ForEach(s => Console.WriteLine(s));
                    int input = GetInput(subsubtext.Length);
                    switch (input)
                    {
                        case 0:
                            Graphviz.PrintGraph(automaat, "test");
                            break;
                        case 1:
                            var gram = RegGram<int>.NdfaToRegGram(automaat);
                            Console.WriteLine(gram.ToString());
                            Console.ReadLine();
                            break;
                        case 2:
                            running = false;
                            automates.Clear();
                            break;
                    }
                }
            }
        }

        private class Minimalization : SubMenu
        {
            private List<AutomateStruct> automates = new List<AutomateStruct>();
            private string[] actions = { "0) DFA", "1) Minimalizeer Hopcroft", "2) Minimalizeer dubbele reverse" , "3) Terug"};

            public void ShowMenu()
            {
                FillList();
                Console.Clear();
                Console.WriteLine("Minimalizatie gekozen \nSelecteer een automaat");
                int index = 0;
                automates.ForEach(a => { Console.WriteLine($"{index}) {a.text}"); index++; });
                index = GetInput(automates.Count);
                HandleSubMenu(automates[index]);
                
            }

            private void FillList()
            {
                automates = new List<AutomateStruct>();
                char[] alphabet = { 'a', 'b' };
                var m1 = new AutomateStruct();
                m1.text = "eindigt op abb";
                var a1 = new Automaat<int>(alphabet);
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 2));
                a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 4));
                a1.AddTransition(new Transition<int>(2, alphabet[0], 3));
                a1.AddTransition(new Transition<int>(3, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(4, alphabet[1], 5));
                a1.AddTransition(new Transition<int>(5, Transition<int>.Epsilon, 6));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 7));
                a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 1));
                a1.AddTransition(new Transition<int>(7, alphabet[0], 8));
                a1.AddTransition(new Transition<int>(8, alphabet[1], 9));
                a1.AddTransition(new Transition<int>(9, alphabet[1], 10));
                a1.DefineAsStartState(0);
                a1.DefineAsFinalState(10);
                m1.automaat = a1;
                automates.Add(m1);
            }

            private void HandleSubMenu(AutomateStruct automaat)
            {
                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"{automaat.text} gekozen \nKies een actie");
                    actions.ToList().ForEach(s => Console.WriteLine(s));
                    int input = GetInput(actions.Length);

                    switch (input)
                    {
                        case 0:
                            automaat.automaat.ViewImage();
                            break;
                        case 1:
                            var minimizedh = automaat.automaat.MinimizeHopCroft();
                            minimizedh.ViewImage();
                            break;
                        case 2:
                            var minimizedr = automaat.automaat.MinimizeReverse();
                            minimizedr.ViewImage();
                            break;
                        case 3:
                            running = false;
                            automates.Clear();
                            break;
                    }
                }
            }
        }
    }
}

