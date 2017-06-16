using System;
using System.Collections.Generic;
using System.Linq;

namespace MontyHall
{
    class Program
    {
        static void Main(string[] args)
        {
            var monty = new MontyHall();

            monty.CountWin();

            Print();
        }

        private static void Print()
        {
            while (true) { }
        }
    }


    // モンティホールクラス
    class MontyHall
    {
        #region インスタンス変数
        private List<string> _doors;
        private string _firstSelect;
        private string _finalSelect;
        private int _noChangeWinCount;
        private int _changeWinCount;
        #endregion

        #region 固定パラメータ
        private const int ProcessTimes = 1000000;
        #endregion

        public MontyHall()
        {
            // アタリ回数の初期化
            _noChangeWinCount = 0;
            _changeWinCount = 0;
        }

        // 未変更、変更時のアタリをカウント
        public void CountWin()
        {
            Enumerable.Range(0, ProcessTimes).ToList().ForEach(_=>
            {
                this.CommonProcess();

                // 変更しない場合
                this.CountNoWinChnage();

                // 変更した場合
                this.CountWinChange();
            });

            var noChangeWinProbability = (double)_noChangeWinCount / ProcessTimes * 100.0;
            var changeWinProbability = (double)_changeWinCount / ProcessTimes * 100.0;

            Console.WriteLine("変更しなかった場合のアタリ回数：" + _noChangeWinCount);
            Console.WriteLine("変更した場合のアタリ回数：" + _changeWinCount);
            Console.WriteLine("変更しなかった場合のアタリ確率：" + noChangeWinProbability + "%");
            Console.WriteLine("変更した場合のアタリ確率：" + changeWinProbability + "%");
        }

        // 共通プロセス
        private void CommonProcess()
        {
            // リスト初期化、アタリ１つ、ハズレ２つのリスト
            _doors = new List<string>() { "o", "x", "x" };

            // 最初の選択
            _firstSelect = _doors.GetRandom<string>();

            // 選択したものをリストから削除
            _doors.Remove(_firstSelect);

            // 残ったリストからハズレ１つを削除
            _doors.Remove("x");
        }

        // 変更しない場合
        private void CountNoWinChnage()
        {
            _finalSelect = _firstSelect;

            if (_finalSelect == "o") { _noChangeWinCount++; }
        }

        // 変更する場合
        private void CountWinChange()
        {
            _finalSelect = _doors.FirstOrDefault();

            if (_finalSelect == "o") { _changeWinCount++; }
        }
    }

    // リストを拡張。リストから1つランダムで取得
    static class ListExtension
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list.Skip(new Random().Next(0, list.Count)).First();
        }
    }

}
