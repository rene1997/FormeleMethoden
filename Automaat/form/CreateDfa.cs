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

            CreateDfaFromData("bbab", "ab", AutomaatGenerator.AutomaatType.BEGINT_MET, true);
            CreateDfaFromData("aba", "ab", AutomaatGenerator.AutomaatType.BEVAT, false);
            CreateDfaFromData("baab", "ab", AutomaatGenerator.AutomaatType.EINDIGT_OP, false);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            var patroon = this.patroonTextBox.Text;
            var alfabet = this.alfabetTextBox.Text;
            var type = (AutomaatGenerator.AutomaatType)this.dfaTypeCombobox.SelectedItem;
            var isNot = this.isNietCheckBox.Checked;
            if (CreateDfaFromData(patroon, alfabet, type, isNot))
                Router.Instance.RouteTo(Router.FormId.Default);
        }

        private bool CreateDfaFromData(string patroon, string alfabet, AutomaatGenerator.AutomaatType type, bool isNot)
        {
            if (string.IsNullOrEmpty(patroon) || string.IsNullOrWhiteSpace(patroon)
                || string.IsNullOrEmpty(alfabet) || string.IsNullOrWhiteSpace(alfabet))
            {
                return false;
            }

            var dfa = AutomaatGenerator.GenerateAutomaat(patroon, alfabet.ToCharArray(), type);
            if (isNot) dfa = !dfa;
            var notString = isNot ? " Not " : " ";
            Store.Instance.ListOfDfas.Add(new Tuple<string, Automaat<int>>($"{type}{notString}{patroon}", dfa));
            return true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Router.Instance.RouteTo(Router.FormId.Default);
        }
    }
}
