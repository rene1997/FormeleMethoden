using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Automaat.form
{
    public partial class CreateDfa : Form
    {
        public CreateDfa()
        {
            InitializeComponent();
            this.dfaTypeCombobox.DataSource = Enum.GetValues(typeof(AutomaatGenerator.AutomaatType));
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (CreateDfaFromData()) 
                Router.Instance.RouteTo(Router.FormId.Default);
        }

        private bool CreateDfaFromData()
        {
            var patroon = this.patroonTextBox.Text;
            var alfabet = this.alfabetTextBox.Text;
            var type = (AutomaatGenerator.AutomaatType) this.dfaTypeCombobox.SelectedItem;
            if (string.IsNullOrEmpty(patroon) || string.IsNullOrWhiteSpace(patroon)
                || string.IsNullOrEmpty(alfabet) || string.IsNullOrWhiteSpace(alfabet))
            {
                return false;
            }

            var dfa = AutomaatGenerator.GenerateAutomaat(patroon, alfabet.ToCharArray(), type);
            Store.Instance.ListOfDfas.Add(new Tuple<string, Automaat<int>>($"{type} {patroon}", dfa));
            return true;
        }
    }
}
