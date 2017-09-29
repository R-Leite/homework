using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleepSort
{
    class Answer5
    {
        #region 定数
        private const int Unit = 100;
        #endregion

        public IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new SynchronizedCollection<int>();

            var taskList = valueList.Select(x => Task.Run(async () =>
            {
                await Task.Delay(x * Unit);
                output.Add(x);
            }));

            Task.WaitAll(taskList.ToArray());

            return output;
        }
    }
}
