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

        // データ保存クラスのインスタンス化
        protected SaveData _dataStore;

        // 設定を保存するXMLファイル名
        protected string _xmlFileName = @"NumberingSystem_SaveData.xml";
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

        #region デシリアライズ
        /// <summary>
        /// アプリケーションの設定をXMLファイルから取り込む
        /// </summary>
        protected bool LoadData()
        {
            // ファイルが存在しているかどうか確認し、なければデフォルト値で作成する
            if (!System.IO.File.Exists(_xmlFileName))
            {
                _dataStore = new SaveData();
                return false;
            }

            // XmlSerializerオブジェクトの作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
            System.IO.StreamReader sr = new System.IO.StreamReader(
                _xmlFileName, new System.Text.UTF8Encoding(false));
            _dataStore = (SaveData)serializer.Deserialize(sr);
            sr.Close();

            return true;
        }
        #endregion

        #region シリアライズ
        /// <summary>
        /// アプリケーションの設定をXMLファイルに保存する
        /// </summary>
        protected void StoreData()
        {
            //XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                _xmlFileName, false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, _dataStore);
            sw.Close();
        }
        #endregion
    }
}
