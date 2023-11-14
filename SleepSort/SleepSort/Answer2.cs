using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SleepSort
{
    /// <summary>
    /// WhenAllでタスク完了を待機
    /// out修飾子を使用
    /// </summary>
    class Answer2
    {
        #region 定数
        private const int Unit = 1000;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var task = SortTask(valueList, out output);
            task.Wait();

            return output;
        }

        public Task SortTask(IEnumerable<int> valueList, out List<int> output)
        {
            var ansList = new List<int>();
            var taskList = new List<Task>();
            foreach (var i in valueList)
            {
                var task = Task.Run(() => ansList.Add(SleepAdd(i)));
                taskList.Add(task);
            }

            output = ansList;

            // 全タスクが完了した時に完了扱いになるタスク
            return Task.WhenAll(taskList);
        }

        private int SleepAdd(int num)
        {
            Thread.Sleep(num * Unit);
//            Console.WriteLine(num);
            return num;
        }
    }
}
