using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCompetition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

            try
            {
                Console.WriteLine("テニスの対戦組み合わせ表を出力します");
                var p1 = new Competition();

                // 全ペアを作成する
                for(var i = 0; i < 8; i++)
                {
                    for (var j = i+1; j < 8; j++)
                    {
                        Console.WriteLine(i.ToString()+","+j.ToString());
                    }
                }
            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押してください。");
                Console.ReadKey();
            }
        }
    }
}
