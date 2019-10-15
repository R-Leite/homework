using System;
using System.Collections.Generic;
using System.Linq;

namespace NewEmployeePractice9
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringList1 = new List<String>() { "apple", "orange", "banana" };
            var stringList2 = new List<String>() { "japan", "america", "brazil", "china" };

            var answer = stringList1.Zip(stringList2, (a, b) => $"{a}:{b}");

            Console.WriteLine(answer.Aggregate((a, b) => $"{a}, {b}"));
            Console.WriteLine("続行するには何かキーを押して下さい。");
            Console.ReadKey();
        }
    }
}
