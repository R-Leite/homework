using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NumberingSystem.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region インスタンス変数
        /// <summary> 
        /// プロパティに紐づいたエラーメッセージ格納用の辞書 
        /// </summary> 
        protected Dictionary<string, string> _errors = new Dictionary<string, string>();
        #endregion

        #region プロパティ
        /// <summary>
        /// IDataErrorInfo.Errorの実装
        /// </summary>
        public string Error { get { return null; } }

        /// <summary> 
        /// columnNameで指定したプロパティのエラー 
        /// </summary> 
        /// <param name="columnName">プロパティ名</param> 
        /// <returns>エラーメッセージ</returns> 
        public string this[string columnName]
        {
            get { return (this._errors.ContainsKey(columnName) ? this._errors[columnName] : null); }
        }
        #endregion

        #region イベントハンドラ
        /// <summary> 
        /// プロパティの変更時イベント
        /// </summary> 
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> 
        /// バインドされたプロパティのイベントハンドラ
        /// </summary> 
        /// <param name="propertyName">プロパティ名</param> 
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }
        #endregion

        #region エラーハンドリング
        /// <summary> 
        /// プロパティにエラーメッセージを設定する。 
        /// </summary> 
        /// <param name="propertyName">プロパティ名</param> 
        /// <param name="errorMessage">エラーメッセージ</param> 
        protected void SetError(string propertyName, string errorMessage)
        {
            this._errors[propertyName] = errorMessage;
            this.RaisePropertyChanged(propertyName);
        }

        /// <summary> 
        /// プロパティのエラーをクリアする。 
        /// </summary> 
        /// <param name="propertyName">プロパティ名</param> 
        protected void ClearError(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
            {
                this._errors.Remove(propertyName);
            }
            this.RaisePropertyChanged(propertyName);
        }
        #endregion
    }
}
