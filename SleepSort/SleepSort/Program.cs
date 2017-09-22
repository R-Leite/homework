using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("スペース区切りで数値を入力:");
            var input = Console.ReadLine().Split(' ').Select(x => int.Parse(x));

            Sort(input);

            Console.WriteLine("終了するにはEnterキーを押して下さい。");
            Console.ReadLine();
        }

        static IEnumerable<int> Sort(IEnumerable<int> valueList)
        {
            var output = new List<int>();
            var taskList = new List<Task>();
            foreach (var i in valueList)
            {
                output.Add(Task.Run(() => SleepAdd(i)));
            }

            return output;
        }

        async static Task<int> SleepAdd(int num)
        {
            System.Threading.Thread.Sleep(num*1000);
            Console.WriteLine(num);
            return num;
        }
    }
}
