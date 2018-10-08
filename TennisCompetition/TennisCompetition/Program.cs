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
                while(true)
                {
                    var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

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
