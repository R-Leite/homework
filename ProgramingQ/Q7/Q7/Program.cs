using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("現在温度：");
            var currentTemp = int.Parse(Console.ReadLine());
            Console.Write("設定温度：");
            var settingTemp = int.Parse(Console.ReadLine());

            var max = 35;
            var min = 15;
            var operate = new List<int>() { -10, 10, -5, 5, -1, 1 };
            var already = new List<int>();
            var queue = new Queue<Tuple<string, int>>();
            var count = 0;

            queue.Enqueue(Tuple.Create<string, int>("", currentTemp));

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
//                Console.WriteLine(currentNode.Item1);
                var operateTemp = operate.Select(x => Tuple.Create<string, int>(currentNode.Item1 + x.ToString(), x + currentNode.Item2)).Where(x => x.Item2 <= max).Where(x => x.Item2 >= min).Where(x => !already.Contains(x.Item2)).ToList();
                if (operateTemp.Select(x => x.Item2).Contains(settingTemp))
                {
                    Console.WriteLine(currentNode.Item1);
                    break;
                }
                already.AddRange(operateTemp.Select(x => x.Item2));

                operateTemp.ForEach(x => queue.Enqueue(x));
            }

            Console.WriteLine(count);
            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }
    }
}
