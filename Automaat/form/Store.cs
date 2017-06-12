using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat.form
{
    class Store
    {
        public List<string> ListOfDfas { get; }
        public Store()
        {
            ListOfDfas = new List<string>
            {
                "DFA1",
                "DFA2"
            };
        }
    }
}
