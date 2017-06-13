using Automaat.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automaat
{
    public struct AutomateStruct
    {
        public Automaat<int> automaat;
        public string text;
    }
    

    public struct RegexStruct
    {
        public RegExp regex;
        public RegExp regex2;
        public string text;
    }

    public static class Samples
    {
        static char[] alphabet = { 'a', 'b' };

        

        public static List<AutomateStruct> GetSamples()
        {
            var listStruct = new List<AutomateStruct>();
            listStruct.Add(new AutomateStruct {automaat = GetEndWithABB(), text = "eindigt op 'abb'" });
            listStruct.Add(new AutomateStruct { automaat = GetStartWithABB(), text = "begint met 'abb'" });
            listStruct.Add(new AutomateStruct { automaat = GetContainsABB(), text = "bevat 'abb'" });
            listStruct.Add(new AutomateStruct { automaat = GetStartAbbOrEndBaab(), text = "begint met 'abb' of eindigt met 'baab'" });
            listStruct.Add(new AutomateStruct { automaat = GetEvenBOrUnevenA(), text = "Oneven aantal a's of Even aantal b's" });
            return listStruct;
        }

        public static List<RegexStruct> GetRegexs()
        {
            var listStruct = new List<RegexStruct>();
            var a = new RegExp("a");
            var b = new RegExp("b");
            var int1 = "aba(a*|b*)* begint met aba";
            var reg1 = a.dot(b).dot(a).dot(a.or(b).star());
            var reg2 = new RegExp("a").dot(new RegExp("a").star()).dot(new RegExp("b").dot(new RegExp("b").star()));
            var reg3 = new RegExp("a").or(new RegExp("b"));
            var reg4 = new RegExp("a").plus().dot(new RegExp("a").star()).dot(new RegExp("b").plus());
            var reg5 = new RegExp("a").dot(new RegExp("a").star()).dot(new RegExp("b").dot(new RegExp("b").star()));
            listStruct.Add(new RegexStruct { text = int1, regex = reg1 });
            listStruct.Add(new RegexStruct { regex = reg2, text = "a+b+" });
            listStruct.Add(new RegexStruct { regex = reg3.star(), text = "(a|b)*" });
            listStruct.Add(new RegexStruct { regex = reg4, text = "a+(a*)b+" });
            listStruct.Add(new RegexStruct { regex = reg5, text = "a a* b b*" });
            return listStruct;
        }

        private static Automaat<int> GetEndWithABB()
        {
            var a1 = new Automaat<int>(alphabet);
            a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 1)); a1.AddTransition(new Transition<int>(0, Transition<int>.Epsilon, 7));
            a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 2)); a1.AddTransition(new Transition<int>(1, Transition<int>.Epsilon, 4));
            a1.AddTransition(new Transition<int>(2, alphabet[0], 3)); a1.AddTransition(new Transition<int>(3, Transition<int>.Epsilon, 6));
            a1.AddTransition(new Transition<int>(4, alphabet[1], 5)); a1.AddTransition(new Transition<int>(5, Transition<int>.Epsilon, 6));
            a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 7)); a1.AddTransition(new Transition<int>(7, alphabet[0], 8));
            a1.AddTransition(new Transition<int>(6, Transition<int>.Epsilon, 1)); a1.AddTransition(new Transition<int>(8, alphabet[1], 9));
            a1.AddTransition(new Transition<int>(9, alphabet[1], 10));
            a1.DefineAsStartState(0);
            a1.DefineAsFinalState(10);
            return a1;
        }

        private static Automaat<int> GetStartWithABB()
        {
            var a1 = new Automaat<int>(alphabet);
            a1.AddTransition(new Transition<int>(0, alphabet[0], 1));
            a1.AddTransition(new Transition<int>(1, alphabet[1], 2));
            a1.AddTransition(new Transition<int>(2, alphabet[1], 3));
            a1.AddTransition(new Transition<int>(3, alphabet[0], 3));
            a1.AddTransition(new Transition<int>(3, alphabet[1], 3));
            a1.DefineAsStartState(0);
            a1.DefineAsFinalState(3);
            return a1;
        }

        private static Automaat<int> GetContainsABB()
        {
            var a1 = new Automaat<int>(alphabet);
            a1.AddTransition(new Transition<int>(0, alphabet[0], 1));
            a1.AddTransition(new Transition<int>(0, alphabet[1], 0));
            a1.AddTransition(new Transition<int>(1, alphabet[0], 1));
            a1.AddTransition(new Transition<int>(1, alphabet[1], 2));
            a1.AddTransition(new Transition<int>(2, alphabet[0], 1));
            a1.AddTransition(new Transition<int>(2, alphabet[1], 3));
            a1.AddTransition(new Transition<int>(3, alphabet[0], 3));
            a1.AddTransition(new Transition<int>(3, alphabet[1], 3));
            a1.DefineAsStartState(0);
            a1.DefineAsFinalState(3);
            return a1;
        }

        private static Automaat<int> GetStartAbbOrEndBaab()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<int> m = new Automaat<int>(alphabet);
            m.AddTransition(new Transition<int>(1, 'a', 2));
            m.AddTransition(new Transition<int>(1, 'b', 5));
            m.AddTransition(new Transition<int>(2, 'a', 9));
            m.AddTransition(new Transition<int>(2, 'b', 3));
            m.AddTransition(new Transition<int>(3, 'a', 6));
            m.AddTransition(new Transition<int>(3, 'b', 4));
            m.AddTransition(new Transition<int>(4, 'a', 4));
            m.AddTransition(new Transition<int>(4, 'b', 4));
            m.AddTransition(new Transition<int>(5, 'a', 6));
            m.AddTransition(new Transition<int>(5, 'b', 5));
            m.AddTransition(new Transition<int>(6, 'a', 7));
            m.AddTransition(new Transition<int>(6, 'b', 5));
            m.AddTransition(new Transition<int>(7, 'a', 9));
            m.AddTransition(new Transition<int>(7, 'b', 8));
            m.AddTransition(new Transition<int>(8, 'a', 6));
            m.AddTransition(new Transition<int>(8, 'b', 5));
            m.AddTransition(new Transition<int>(9, 'a', 9));
            m.AddTransition(new Transition<int>(9, 'b', 5));
            m.DefineAsStartState(1);
            m.DefineAsFinalState(4);
            m.DefineAsFinalState(8);
            return m;
        }

        private static Automaat<int> GetEvenBOrUnevenA()
        {
            char[] alphabet = { 'a', 'b' };
            Automaat<int> m = new Automaat<int>(alphabet);

            m.AddTransition(new Transition<int>(1, alphabet[0], 2));
            m.AddTransition(new Transition<int>(1, alphabet[1], 3));

            m.AddTransition(new Transition<int>(2, alphabet[0], 1));
            m.AddTransition(new Transition<int>(2, alphabet[1], 5));

            m.AddTransition(new Transition<int>(3, alphabet[0], 2));
            m.AddTransition(new Transition<int>(3, alphabet[1], 4));

            m.AddTransition(new Transition<int>(4, alphabet[0], 2));
            m.AddTransition(new Transition<int>(4, alphabet[1], 4));

            m.AddTransition(new Transition<int>(5, alphabet[0], 1));
            m.AddTransition(new Transition<int>(5, alphabet[1], 4));

            // only on start state in a dfa:
            m.DefineAsStartState(1);

            // two final states:
            m.DefineAsFinalState(2);
            m.DefineAsFinalState(4);
            m.DefineAsFinalState(5);
            return m;
        }


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

        string[] Hoofdmenu = {
            "regex => ndfa",
            "ndfa => dfa",
            "ndfa => grammatica",
            "dfa => minimalisatie",
            "thompson",
            "gelijkheid reguliere expressies",
            "maak zelf een automaat"
        };

        private readonly List<SubMenu> _submenus = new List<SubMenu>();

        public UserProgram()
        {
            FillSubmenus();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("welkom, voer een getal in om een functie uit te voeren:");
                int index = 0;
                Hoofdmenu.ToList().ForEach(s => { Console.WriteLine($"{index}) {Hoofdmenu[index]}"); index++; });
                index = GetInput(_submenus.Count);
                _submenus[index].ShowMenu();
            }
            
        }

        public void FillSubmenus()
        {
            _submenus.Add(new RegToNDFA());
            _submenus.Add(new NDFAToDFA());
            _submenus.Add(new NdfaToGrammatica());
            _submenus.Add(new Minimalization());
            _submenus.Add(new ThompsonSample());
            _submenus.Add(new CompareRegex());
            //_submenus.Add(new BuildDfa());
            _submenus.Add(new ShowGUI());
        }

        private interface SubMenu
        {
            void ShowMenu();
        }

        private class RegToNDFA : SubMenu
        {
            List<RegexStruct> sampleRegex = new List<RegexStruct>();
            public void ShowMenu()
            {
                FillSamples();
                Console.Clear();
                Console.WriteLine("regex => ndfa gekozen \nkies een regex");
                int index = 0;
                sampleRegex.ForEach(r => { Console.WriteLine($"{index}) {r.text}");  index++; });
                index = GetInput(sampleRegex.Count);
                if (index >= 0)
                    ShowRegex(index);
            }

            private void FillSamples()
            {
                sampleRegex =Samples.GetRegexs();
            }

            private void ShowRegex(int index)
            {
                var regex = sampleRegex[index].regex;

                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"regex naar ndfa: " + sampleRegex[index].text);
                    Console.WriteLine("automaat:");
                    var automaat = Thompson.CreateAutomaat(regex);
                    automaat.Print();

                    Console.WriteLine("\n\n");
                    Console.WriteLine("kies een volgende actie:");
                    Console.WriteLine("0) toon afbeelding automaat");
                    Console.WriteLine("1) toon afbeelding dfa");
                    Console.WriteLine("2) toon afbeelding geminimaliseerde automaat");
                    Console.WriteLine("3) toon geaccepteerde woorden");
                    Console.WriteLine("4) toon niet geaccepteerde woorden");
                    Console.WriteLine("5) terug naar hoofdmenu");
                    index = GetInput(6);
                    HandleSubMenu(index, automaat);
                    if (index < 0 || index > 4)
                    {
                        running = false;
                        sampleRegex.Clear();
                    }
                        
                }
            }

            private void HandleSubMenu(int index, Automaat<int> automaat) 
            {
                switch (index)
                {
                    case 0:
                        automaat.ViewImage();
                        break;
                    case 1:
                        NdfatoDfa.MakeDfa(automaat).ViewImage();
                        break;
                    case 2:
                        automaat.MinimizeHopCroft(false).ViewImage();
                        break;
                    case 3:
                        automaat.GeefTaal(5).ForEach(s => Console.WriteLine(s));
                        Console.ReadLine();
                        break;
                    case 4:
                        automaat.GeefNietTaal(5).ForEach(s => Console.WriteLine(s));
                        Console.ReadLine();
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
                automates = Samples.GetSamples();
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
                            Graphviz.PrintGraph(dfa.MinimizeHopCroft(false), "test");
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
                automates = Samples.GetSamples();
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
                automates = Samples.GetSamples();
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
                            Console.Clear();
                            Console.WriteLine("laatste minimalisatie tabel: \t druk op enter om het bijbehorende automaat te zien");
                            var minimizedh = automaat.automaat.MinimizeHopCroft(true);
                            Console.ReadLine();
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

        private class ThompsonSample : SubMenu
        {
            private List<RegexStruct> samples = new List<RegexStruct>();
            private string[] actions = { "0) automaat met thompson" ,"1) DFA" , "2) Geminimalizeerd", "3) terug"};

            public void ShowMenu()
            {
                FillList();
                Console.Clear();
                Console.WriteLine("thomson gekozen. Kies een regex");
                int index = 0;
                samples.ForEach(s => { Console.WriteLine($"{index}) {s.text}"); index++; });
                index = GetInput(samples.Count);
                ViewActions(samples[index]);

            }

            public void FillList()
            {
                samples = Samples.GetRegexs();

            }

            private void ViewActions(RegexStruct regexStruct)
            {
                var running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"regex: {regexStruct.text} gekozen. Kies een actie");
                    actions.ToList().ForEach(a => Console.WriteLine(a));
                    int input = GetInput(actions.Length);
                    switch (input)
                    {
                        case 0:
                            Thompson.CreateAutomaat(regexStruct.regex).ViewImage();
                            break;
                        case 1:
                            NdfatoDfa.MakeDfa(Thompson.CreateAutomaat(regexStruct.regex)).ViewImage();
                            break;
                        case 2:
                            NdfatoDfa.MakeDfa(Thompson.CreateAutomaat(regexStruct.regex)).MinimizeHopCroft(false).ViewImage();
                            break;
                        case 3:
                            running = false;
                            samples.Clear();
                            break;
                    }
                }
            }
        }

        private class CompareRegex : SubMenu
        {
            private List<RegexStruct> samples = new List<RegexStruct>();
            private string[] actions = { "0) automaat 1", "1) automaat 2", "2) controleer gelijkheid", "3) terug" };

            public void ShowMenu()
            {
                FillList();
                Console.Clear();
                Console.WriteLine("Vergelijk regex gekozen: \nKies een voorbeeld:");
                int index = 0;
                samples.ForEach(s => { Console.WriteLine($"{index}) {s.text}"); index++; });
                Console.WriteLine( "voer eerste regex in:");
                var index1 = GetInput(samples.Count);
                Console.WriteLine("voer tweede regex in:");
                var index2 = GetInput(samples.Count);

                HandleAction(samples[index1], samples[index2]);
            }

            private void FillList()
            {
                samples = Samples.GetRegexs();
            }

            private void HandleAction(RegexStruct regex1, RegexStruct regex2)
            {
                var running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine($"regex: {regex1.text} en {regex2.text} gekozen. Kies een actie");
                    actions.ToList().ForEach(a => Console.WriteLine(a));
                    int input = GetInput(actions.Length);
                    switch (input)
                    {
                        case 0:
                            Thompson.CreateAutomaat(regex1.regex).ViewImage();
                            break;
                        case 1:
                            Thompson.CreateAutomaat(regex2.regex).ViewImage();
                            break;
                        case 2:
                            Console.WriteLine("gelijkheid reg1 en reg2 is: " + regex1.regex.Equals(regex2.regex2));
                            Console.WriteLine("druk op enter om door te gaan");
                            Console.ReadLine();
                            break;
                        case 3:
                            running = false;
                            samples.Clear();
                            break;
                    }
                }
            }
        }

        private class BuildDfa : SubMenu
        {
            private Array _types;
            private Automaat<int> _buildedAutomaat;
            private string[] submenus;
            public void ShowMenu()
            {
                Console.Clear();
                Init();
                Console.WriteLine("Kies automaat type");

                for (var i = 0; i < _types.Length; i++)
                {
                    Console.WriteLine($"{i}) {_types.GetValue(i)}");
                }
                var typeId = GetInput(_types.Length);

                var valid = false;
                string rules = "", alphabet = "";
                while (!valid)
                {
                    Console.WriteLine("Voer automaat definitie in");
                    rules = Console.ReadLine();

                    Console.WriteLine("Voer alfabet in");
                    alphabet = Console.ReadLine();

                    valid = !string.IsNullOrEmpty(rules);
                }

                BuildAutomaat(rules, alphabet, typeId);

                var runnig = true;
                while (runnig)
                {
                    for (int i = 0; i < this.submenus.Length; i++)
                    {
                        Console.WriteLine($"{i}) {this.submenus[i]}");
                    }
                    var input = GetInput(this.submenus.Length);
                    if (input == 0)
                    {
                        this._buildedAutomaat.ViewImage($"{_types.GetValue(typeId)} {rules}");
                    }
                    runnig = input != submenus.Length-1;
                }
            }

            private void Init()
            {
                _types = Enum.GetValues(typeof(AutomaatGenerator.AutomaatType));
                this.submenus = new []
                {
                    "Toon automaat",
                    "Terug naar hoofdmenu"
                };
            }

            private void BuildAutomaat(string ruleint, string alphabetint, int typeIndex)
            {
                var a = new SortedSet<char>(alphabetint);
                ruleint.ToCharArray().ToList().ForEach(c => a.Add(c));
                char[] alphabet = new char[a.Count];
                for (int i = 0; i < a.Count; i++)
                {
                    alphabet[i] = a.ElementAt(i);
                }
                var type = (AutomaatGenerator.AutomaatType) _types.GetValue(typeIndex);

                this._buildedAutomaat =
                    AutomaatGenerator.GenerateAutomaat(ruleint, alphabet, type);
            }
        }

        private class ShowGUI : SubMenu
        {
            private bool _init = false;
            public void ShowMenu()
            {
                if (!_init)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new DoubleR_FM());
                    _init = true;
                }

                int input = GetInput(1);
            }
        }
    }
}

