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
    public partial class CreateDfa : Form
    {
        public CreateDfa()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Router.Instance.RouteTo(Router.FormId.Default);
        }
    }
}
