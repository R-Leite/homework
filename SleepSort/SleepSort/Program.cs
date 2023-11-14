using System;
using System.Linq;

namespace SleepSort
{
    class Program
    {
        private const int Range = 10;
        private const int ListNumber = 100;

        static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 200;
            var ans1 = new Answer1();
            var ans2 = new Answer2();
            var ans3 = new Answer3();
            var ans4 = new Answer4();
            var ans5 = new Answer5();

            try
            {
#if true
                Console.Write("スペース区切りで数字を入力して下さい:");
                var input = Console.ReadLine().Split(' ').Select(x => Int32.Parse(x));
#else
            var input = Enumerable.Repeat(new Random(), ListNumber).Select(x => x.Next(1, Range)).ToList();
#endif

                Console.WriteLine("入力:" + input.Select(x => x.ToString()).Aggregate((s, n) => s + ", " + n));
                Console.WriteLine("入力個数:" + input.Count());

                //var output = ans1.Sort(input);
                //var output = ans2.Sort(input);
                //var output = ans3.Sort(input);
                //var output = ans4.Sort(input);
                var output = ans5.Sort(input);

                Console.WriteLine();
                Console.WriteLine("出力:" + output.Select(x => x.ToString()).Aggregate((f, s) => f + ", " + s));
                Console.WriteLine("出力個数:" + output.Count());
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            finally
            {
                Console.WriteLine("続行するには何かキーを押して下さい。");
                Console.ReadKey();
            }
        }
    }
}
