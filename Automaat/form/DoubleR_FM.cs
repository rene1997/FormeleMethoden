using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automaat.form
{
    public partial class DoubleR_FM : Form
    {
        public DoubleR_FM()
        {
            InitializeComponent();
            BindStoreToComponents();
            InitRouter();
        }

        private void InitRouter()
        {
            Router.Instance.AddRoute(Router.FormId.Default, this);
            Router.Instance.AddRoute(Router.FormId.CreateDfa, new CreateDfa());
            Router.Instance.RouteTo(Router.FormId.Default);
        }

        private void BindStoreToComponents()
        {
            this.listOfDfas.DataSource = Store.Instance.ListOfDfas;
            this.listOfDfas.DisplayMember = "Item1";
        }

        private void createDfaButton_Click(object sender, EventArgs e)
        {
            Router.Instance.RouteTo(Router.FormId.CreateDfa);
        }

        private void listOfDfas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex != -1)
            {
                //Console.WriteLine(this.listOfDfas.SelectedValue);
            }
        }

        private void toonDfaButton_Click(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex == -1) return;

            var tuple = this.listOfDfas.SelectedItem as Tuple<string, Automaat<int>>;
            tuple?.Item2.ViewImage(tuple.Item1);
        }
    }
}
