using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AokiQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 全てのプロセスを列挙する
            //foreach (var p in System.Diagnostics.Process.GetProcessesByName("MftHmi"))
            //{
            //    Console.WriteLine(p.Id + ":" + p.ProcessName);
            //    var app = p;
            //    Console.WriteLine(app.Handle);
            //}
            //Console.ReadLine();

            var player = new Player(0, 0);

            while(true)
            {
                Console.Write("入力：");
                var input = Console.ReadLine();

                player.Walk(input);

                Console.WriteLine(player._point.X);

            }
        }
    }
}
