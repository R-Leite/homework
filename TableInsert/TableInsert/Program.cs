using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            var tab = new List<Dictionary<string, string>>();
            var dic = new Dictionary<string, string>();
            var dic2 = new Dictionary<string, string>();
            dic.Add("a", "hoge");
            dic.Add("b", "fuga");
            dic2.Add("a", "h");
            dic2.Add("b", "f");
            tab.Add(dic);
            tab.Add(dic2);

            var foo = tab.Select(x => x.Select(y => y.Key + ", " + y.Value).Aggregate((a, b) => a + ", " + b) + ", " + "a" + "b" + ", " + x["a"] + x["b"]);

            foreach (var i in foo)
            {
                Console.WriteLine(i);
            }

            while (true) { }
        }
    }
}
