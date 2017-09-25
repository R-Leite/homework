using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SleepSort
{
    class Answer4
    {
        /// <summary>
        /// ラムダ式版
        /// </summary>
        #region 定数
        private const int Unit = 1000;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var tasks = new List<Task>();

            foreach (var i in valueList)
            {
                var task = Task.Run(() =>
                {
                    Thread.Sleep(i * Unit);
                    Console.WriteLine(i);
                    output.Add(i);
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            return output;
        }
    }
}
