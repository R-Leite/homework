using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SleepSort
{
    class Answer3
    {
        /// <summary>
        /// ラムダ式版
        /// </summary>
        #region 定数
        private const int Unit = 100;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var tasks = new List<Task>();

            var taskList = valueList.Select(x => Task.Run(() => 
            {
                Thread.Sleep(x * Unit);
                //Console.WriteLine(x);
                output.Add(x);
            }));

            Task.WaitAll(taskList.ToArray());
            return output;
        }
    }
}
