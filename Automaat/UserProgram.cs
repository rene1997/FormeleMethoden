using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat
{
    

    public class UserProgram
    {
        String[] Hoofdmenu = new string[] {
            "regex => ndfa",
            "ndfa => dfa",
            "ndfa => grammatica",
            "grammatica => ndfa",
            "dfa => minimalisatie",
            "dfa => revers",
            "thomson",
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
                index = 0;
                var input = Console.ReadLine();
                int.TryParse(input, out index);
                submenus[index].ShowMenu();
            }
            
        }

        public void FillSubmenus()
        {
            submenus.Add(new RegToNDFA());
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
                index = -1;
                var input = Console.ReadLine();
                int.TryParse(input, out index);
                if (index >= 0)
                    ShowRegex(index);
            }

            private void FillSamples()
            {
                var a = new RegExp("a");
                var b = new RegExp("b");
                var string1 = "aba(a*|b*)* begint met aba";
                var reg1 = a.dot(b).dot(a).dot(a.star().or(b.star()));
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
                    index = -1;
                    var input = Console.ReadLine();
                    int.TryParse(input, out index);
                    HandleSubMenu(index, automaat);
                    if (index < 0 || index > 2)
                        running = false;
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
    }
}

