using NumberingSystem.Model;
using System;
using System.Linq;
using System.Windows;

namespace NumberingSystem.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        #region プロパティ
        private string _number;
        public string Number
        {
            get { return _number; }
            set { _number = value; this.RaisePropertyChanged("Number"); }
        }
        #endregion

        #region コマンドプロパティ
        // 番号取得
        public DelegateCommand GetNumberCommand { get; set; }
        #endregion

        #region コンストラクタ
        public MainViewModel()
        {
            Action actionGetNumber = this.GetNumber;
            this.GetNumberCommand = new DelegateCommand(actionGetNumber, () => true);

        }
        #endregion

        #region 番号取得
        private void GetNumber()
        {
            // データを取得
            this.LoadData();

            var numberList = _dataStore.Number.Split('-').ToList();
            var header = "SPES";
            var year = DateTime.Now.ToString("yy");
            if (int.Parse(numberList[1]) >= 999){ MessageBox.Show("これ以上採番できません。"); this.Number = ""; return; }
            var serial = (numberList[0] != year) ? "000" : (int.Parse(numberList[1]) + 1).ToString().PadLeft(3, '0');

            _dataStore.Number = year + "-" + serial;

            this.Number = header + _dataStore.Number;

            // データを保存
            this.StoreData();
        }
        #endregion

    }
}
