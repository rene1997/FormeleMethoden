using System;
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
                Console.WriteLine(this.listOfDfas.SelectedValue);
                // If we also wanted to get the displayed text we could use
                // the SelectedItem item property:
                // string s = ((USState)ListBox1.SelectedItem).LongName;
            }
        }
    }
}
