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
            var ans1 = new Answer1();
            var ans2 = new Answer2();
            var ans3 = new Answer3();
            var ans4 = new Answer4();
            var ans5 = new Answer5();

            Console.Write("スペース区切りで数値を入力:");
            var input = Console.ReadLine().Split(' ').Select(x => int.Parse(x));

            //var output = ans1.Sort(input);

            //var output = ans2.Sort(input);

            //var output = ans3.Sort(input);

            //var output = ans4.Sort(input);

            var output = ans5.Sort(input);

            Console.WriteLine(output.Select(x => x.ToString()).Aggregate((f, s) => f + ", " + s));
            Console.WriteLine("終了するにはEnterキーを押して下さい。");
            Console.ReadLine();
        }
    }
}
