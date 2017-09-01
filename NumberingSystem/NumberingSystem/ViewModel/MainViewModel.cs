using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberingSystem.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region プロパティ
        private string _label;
        public string Label
        {
            get { return _label; }
            set { _label = value; this.RaisePropertyChanged("Label"); }
        }
        #endregion

        public MainViewModel()
        {
            this.Label = "hogehoge";
        }
    }
}
