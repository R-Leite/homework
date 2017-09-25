using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SleepSort
{
    class Answer5
    {
        /// <summary>
        /// これはできてない
        /// </summary>
        #region 定数
        private const int Unit = 1000;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var taskList = new List<Task<int>>();
            foreach (var i in valueList)
            {
                var task = SleepAdd(i);
                taskList.Add(task);
            }

            // resultで値取得しても順番はそのままなので✕
            return taskList.Select(x=>x.Result);
        }

        async private Task<int> SleepAdd(int num)
        {
            await Task.Delay(num * Unit);
            Console.WriteLine(num);
            return num;
        }
    }
}
