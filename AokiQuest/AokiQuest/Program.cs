using System;

namespace AokiQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player(Player.origin.X, Player.origin.Y);

            while(true)
            {
                int input;
                Console.Write("入力：");
                if (!int.TryParse(Console.ReadLine(), out input)) { break; }

                var p = player.Walk((Player.Direction)input);
                Console.WriteLine("プレイヤー位置:({0}, {1})", p.X, p.Y);
            }
        }
    }
}
