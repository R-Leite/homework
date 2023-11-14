using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SleepSort
{
    /// <summary>
    /// SleepAddからの返り値を追加していく
    /// 全てのタスクが終わるまでWaitAllで待機
    /// （WaitAllはデッドロックを発生させる要因となるのであまり使用しない）
    /// </summary>
    class Answer1
    {
        #region 定数
        private const int Unit = 1000;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var taskList = new List<Task>();
            foreach (var i in valueList)
            {
                var task = Task.Run(() => output.Add(SleepAdd(i)));
                taskList.Add(task);
            }

            // 全タスクが終了するまで待機
            Task.WaitAll(taskList.ToArray());

            return output;
        }

        private int SleepAdd(int num)
        {
            Thread.Sleep(num * Unit);
//            Console.WriteLine(num);
            return num;
        }
    }
}
