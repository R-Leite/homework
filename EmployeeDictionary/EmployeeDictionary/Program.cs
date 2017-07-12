using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeData = new List<string>() { "19336", "青木秀樹", "19337", "岩本康宏", "69210", "メーカインウー", "69281", "宇賀勇太" };

            Console.WriteLine(employeeData.Aggregate((a, b) => a + ", " + b));

            PrintAnswer(CreateEmployeeDictionary(employeeData));

            PrintAnswer(Linq(employeeData));

            while (true) { }
        }

        static void PrintAnswer(Dictionary<string, string> dictionary)
        {
            Console.WriteLine(dictionary.Select(x => x.ToString()).Aggregate((a, b) => a + ", " + b));
        }

        static Dictionary<string, string> CreateEmployeeDictionary(List<string> employeeData)
        {
            return employeeData.Select((val, idx) => new { val, idx }).GroupBy(x => (int)(x.idx / 2), x => x.val).ToDictionary(x => x.FirstOrDefault(), x => x.LastOrDefault());
        }

        static Dictionary<string, string> Linq(List<string> employeeData)
        {
            var k = employeeData.Where((val, idx) => (idx % 2 == 0));
            var v = employeeData.Where((val, idx) => (idx % 2 == 1));

            return k.Zip(v, (key, val) => new { key, val }).ToDictionary(x => x.key, x => x.val);
        }
    }
}
