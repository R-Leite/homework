namespace NumberingSystem.ViewModel
{
    /// <summary>
    /// XML形式で外部保存するデータクラス
    /// </summary>
    [System.Xml.Serialization.XmlRoot("ギアシステムの構成設定")]
    public class SaveData
    {
        #region XMLファイルに読み書きする値
        [System.Xml.Serialization.XmlElement("番号")]
        public string Number = "00-000";
        #endregion
    }
}
