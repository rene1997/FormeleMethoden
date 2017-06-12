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
        private readonly Store _store;

        public DoubleR_FM()
        {
            InitializeComponent();
            Router.Instance.AddRoute(Router.FormId.Default, this);
            Router.Instance.RouteTo(Router.FormId.Default);
            
            this._store = new Store();
        }

        private void BindStoreToComponents()
        {
            this.listOfDfas.DataSource = _store.ListOfDfas;
        }

        private void createDfaButton_Click(object sender, EventArgs e)
        {
            if (!Router.Instance.RouteTo(Router.FormId.CreateDfa, true))
            {
                Router.Instance.AddRoute(Router.FormId.CreateDfa, new CreateDfa());
            }

            Router.Instance.RouteTo(Router.FormId.CreateDfa, true);
        }
    }
}
