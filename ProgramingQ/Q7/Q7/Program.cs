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
            var queue = new Queue<int>();
            var count = 0;

            queue.Enqueue(currentTemp);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                Console.WriteLine(currentNode);
                var operateTemp = operate.Select(x => x + currentNode).Where(x => x <= max).Where(x => x >= min).Where(x => !already.Contains(x)).ToList();
                count++;
                if (operateTemp.Contains(settingTemp)) break;
                already.AddRange(operateTemp);

                operateTemp.ForEach(x => queue.Enqueue(x));
            }

            Console.WriteLine(count);
            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }
    }
}
