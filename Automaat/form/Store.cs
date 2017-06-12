using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaat.form
{
    class Store
    {
        private static Store _instance;
        public static Store Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new Store();
                return _instance;
            }
        }


        public BindingList<Tuple<string, Automaat<int>>> ListOfDfas { get; }
        private Store()
        {
            ListOfDfas = new BindingList<Tuple<string, Automaat<int>>>();
        }
    }
}
