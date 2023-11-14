using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SleepSort
{
    class Answer4
    {
        /// <summary>
        /// 複数のtaskを1つのtask化して返す
        /// </summary>
        #region 定数
        private const int Unit = 100;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            return SleepAdd(valueList).Result;
        }

        async private Task<IEnumerable<int>> SleepAdd(IEnumerable<int> valueList)
        {
            var output = new List<int>();

            var taskList = valueList.Select(x => Task.Run(async () =>
            {
                await Task.Delay(x * Unit);
                output.Add(x);
            }));

            await Task.WhenAll(taskList);

            return output;
        }
    }
}
