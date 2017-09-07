using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberingSystem.Model;

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

        #region コマンドプロパティ
        // 番号取得
        public DelegateCommand GetNumberCommand { get; set; }
        #endregion

        #region コンストラクタ
        public MainViewModel()
        {
            this.Label = "0";

            Action actionGetNumber = this.GetNumber;
            this.GetNumberCommand = new DelegateCommand(actionGetNumber, () => true);

            var hoge = Properties.Resources.Number;

            MessageBox.Show(hoge);
        }
        #endregion

        #region 番号取得
        private void GetNumber()
        {
            this.Label = (int.Parse(this.Label) + 1).ToString();
        }
        #endregion

    }
}
