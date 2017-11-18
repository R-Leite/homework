using System;
using System.Collections.Generic;
using System.Linq;

namespace Q7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("現在温度：");
            var currentTemp = int.Parse(Console.ReadLine());

            Console.Write("設定温度：");
            AirConditioner.SettingTemperature =  int.Parse(Console.ReadLine());

            var queue = new Queue<AirConditioner>();

            queue.Enqueue(new AirConditioner(new List<int>(), currentTemp));

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                if(currentNode.isGoal())
                {
                    currentNode.ShowResult();
                    break;
                }

                currentNode.GetChildren().ToList().ForEach(x => queue.Enqueue(x));
            }

            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }
    }
}
