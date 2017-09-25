using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SleepSort
{
    /// <summary>
    /// インスタンス変数を使ったやり方
    /// SleepAddで直接要素を追加
    /// 最大秒数待機
    /// </summary>
    class Answer1
    {
        #region インスタンス変数
        private List<int> _ansList;
        #endregion

        private const int Unit = 1000;

        public Answer1()
        {
            _ansList = new List<int>();
        }

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            foreach (var i in valueList)
            {
                Task.Run(() => SleepAdd(i));
            }

            Thread.Sleep(valueList.Max() * Unit);

            return _ansList;
        }

        private void SleepAdd(int num)
        {
            Thread.Sleep(num * Unit);
            Console.WriteLine(num);
            _ansList.Add(num);
        }
    }
}
