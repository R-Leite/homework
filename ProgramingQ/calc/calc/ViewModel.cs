using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class ViewModel : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private IEnumerable<string> _items;
        public IEnumerable<string> Items
        {
            get { return _items; }
            set { _items = value; this.RaisePropertyChanged("Items"); }
        }

        private bool _check;
        public bool Check
        {
            get { return _check; }
            set
            {
                _check = value;
                if (_check) _items = new List<string>() { "hoge", "fuga", "foo", "bar" };
                else _items = new List<string>();
                this.RaisePropertyChanged("Check");
            }
        }

        public ViewModel()
        {
            _text = "Init";
            _check = true;
            //_items = new List<string>() { "hoge", "fuga", "foo", "bar" };
            _items = new List<string>();
        }
    }
}
