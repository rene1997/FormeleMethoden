using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automaat.form
{
    class Router
    {
        private static Router _instance;
        public static Router Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Router();;
                }
                return _instance;
            }
        }

        public enum FormId
        {
            Default,
            CreateDfa
        }
        private readonly SortedDictionary<FormId, Form> _routes;
        private Form _current;
        private Router()
        {
            this._routes = new SortedDictionary<FormId, Form>();
        }

        public bool RouteTo(FormId id, bool getSizeOfPrevious)
        {
            if (!this._routes.TryGetValue(id, out Form nextForm)) return false;

            _current?.Hide();
            if (_current != null && getSizeOfPrevious)
            {
                nextForm.Width = _current.Width;
                nextForm.Height = _current.Height;
            } 
            _current = nextForm;
            _current.Show();
            return true;
        }

        public bool RouteTo(FormId id)
        {
            return this.RouteTo(id, false);
        }

        public void AddRoute(FormId id, Form f)
        {
            if (!_routes.ContainsKey(id))
            {
                _routes.Add(id, f);
            }
        }

        public void AddRoute(FormId id, Form f, bool routeTo)
        {
            if (!_routes.ContainsKey(id))
            {
                _routes.Add(id, f);
            }

            if (routeTo) this.RouteTo(id);
        }
    }
}
