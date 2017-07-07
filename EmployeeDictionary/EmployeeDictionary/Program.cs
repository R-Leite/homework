using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeData = new List<string>() { "19336", "青木秀樹", "19337", "岩本康宏", "69210", "メーカインウー", "69281", "宇賀勇太" };

            var ans = employeeData.Select((x, i) => new { x, i }).GroupBy(x => (int)(x.i / 2), x => x.x).ToDictionary(x=>x.First(), x=>x.Skip(1).First());

            foreach(var i in ans)
            {
                Console.WriteLine(i.Key);
                Console.WriteLine(i.Value);
            }

            var tab = new List<Dictionary<string, string>>();
            var dic = new Dictionary<string, string>();
            var dic2 = new Dictionary<string, string>();
            dic.Add("a", "hoge");
            dic.Add("b", "fuga");
            dic2.Add("a", "h");
            dic2.Add("b", "f");
            tab.Add(dic);
            tab.Add(dic2);

            var foo = tab.Select(x => x.Select(y => y.Key + ", " + y.Value).Aggregate((a, b) => a + ", " + b) + ", " + "a" + "b" + ", "+ x["a"] + x["b"]);

            foreach(var i in foo)
            {
                Console.WriteLine(i);
            }

            while (true) { }
        }
    }
}
