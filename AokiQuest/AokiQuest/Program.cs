using System;

namespace AokiQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;

            var player = new Player(0, 0);

            while(true)
            {
                Console.Write("入力：");
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    var p = player.Walk(input);
                    Console.WriteLine("プレイヤー位置:({0}, {1})", p.X, p.Y);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
