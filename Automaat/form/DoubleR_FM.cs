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
        private enum DfaOperator
        {
            En,
            Of
        }

        private Tuple<string, Automaat<int>> _left, _right;
        public DoubleR_FM()
        {
            InitializeComponent();

            ResetCombineDfa();

            BindComponents();
            InitRouter();
        }

        private void InitRouter()
        {
            Router.Instance.AddRoute(Router.FormId.Default, this);
            Router.Instance.AddRoute(Router.FormId.CreateDfa, new CreateDfa());
            Router.Instance.RouteTo(Router.FormId.Default);
        }

        private void BindComponents()
        {
            this.listOfDfas.DataSource = Store.Instance.ListOfDfas;
            this.listOfDfas.DisplayMember = "Item1";

            this.combineDfaComboBox.DataSource = Enum.GetValues(typeof(DfaOperator));
            
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

        private void combineDfaSelectButton_Click(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex == -1) return;

            var tuple = this.listOfDfas.SelectedItem as Tuple<string, Automaat<int>>;
            if (tuple == null) return;
            if (_left == null && !Equals(_right, tuple))
            {
                _left = tuple;
                SetCombineLabelString(1, _left.Item1);
            }
            else if (_right == null && !Equals(_left, tuple))
            {
                _right = tuple;
                SetCombineLabelString(2, _right.Item1);
            }
        }

        private void combineDfaResetButton_Click(object sender, EventArgs e)
        {
            ResetCombineDfa();
        }

        private void ResetCombineDfa()
        {
            this._left = null;
            this._right = null;

            SetCombineLabelString(1, "");
            SetCombineLabelString(2, "");
        }

        private void combineDfaCreateButton_Click(object sender, EventArgs e)
        {
            CombineDfas();
        }

        private void CombineDfas()
        {
            var dfaOperator = combineDfaComboBox.SelectedItem;

            if (!(dfaOperator is DfaOperator)) return;
            dfaOperator = (DfaOperator)dfaOperator;

            if (_left == null || _right == null) return;

            Automaat<int> combineDfa = null;
            switch (dfaOperator)
            {
                case DfaOperator.En:
                    combineDfa = _left.Item2 & _right.Item2;
                    break;
                case DfaOperator.Of:
                    combineDfa = _left.Item2 | _right.Item2;
                    break;
            }
            var name = $"{_left.Item1} {dfaOperator} {_right.Item1}";

            if (combineDfa == null) return;
            Store.Instance.ListOfDfas.Add(new Tuple<string, Automaat<int>>(name, combineDfa));
            ResetCombineDfa();
        }

        private void ReplaceTuple(Tuple<string, Automaat<int>> old, Automaat<int> newAutomaat)
        {
            var newTuple = new Tuple<string, Automaat<int>>(old.Item1, newAutomaat);
            Store.Instance.ListOfDfas.Remove(old);
            Store.Instance.ListOfDfas.Add(newTuple);
        }

        private void MinimizeButtonHopCroft_Click(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex == -1) return;

            var tuple = this.listOfDfas.SelectedItem as Tuple<string, Automaat<int>>;
            if (tuple == null) return;
            ReplaceTuple(tuple, tuple.Item2.MinimizeHopCroft(false));
        }

        private void MinimizeButtonReverse_Click(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex == -1) return;

            var tuple = this.listOfDfas.SelectedItem as Tuple<string, Automaat<int>>;
            if (tuple == null) return;
            ReplaceTuple(tuple, tuple.Item2.MinimizeReverse());
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (this.listOfDfas.SelectedIndex == -1) return;

            var tuple = this.listOfDfas.SelectedItem as Tuple<string, Automaat<int>>;
            if (tuple == null) return;
            Store.Instance.ListOfDfas.Remove(tuple);
        }

        private void SetCombineLabelString(int num, string dfaname)
        {
            var labelText = $"Dfa{num}: {dfaname}";
            switch (num)
            {
                case 1:
                    this.combineDfa1Label.Text = labelText;
                    break;
                case 2:
                    this.combineDfa2Label.Text = labelText;
                    break;
            }
        }
    }
}
