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
            var table = new List<Dictionary<string, string>>();
            var dic = new Dictionary<string, string>();
            var dic2 = new Dictionary<string, string>();
            var dic3 = new Dictionary<string, string>();

            dic.Add("a", "hoge");
            dic.Add("b", "fuga");
            dic2.Add("a", "h");
            dic2.Add("b", "f");
            dic3.Add("a", "hoge");
            dic3.Add("b", "foo");
            table.Add(dic);
            table.Add(dic2);
            table.Add(dic3);

            var ans = new Dictionary<string, string>();

            var aa = table.GroupBy(dict => dict["a"]);

            foreach(var i in aa)
            {
                Console.WriteLine(i.Key);
                foreach(var j in i)
                {
                    foreach(var k in j)
                    {
                        Console.WriteLine(k);
                    }
                }
            }

            foreach(var d in table)
            {
                var k = d["a"] + d["b"];
                if (ans.ContainsKey(k)) { ans[k] += d["b"]; }
                else { ans.Add(k, d["b"]); }
            }
            
            var foo = table.Select(x => x.Select(y => y.Key + ", " + y.Value).Aggregate((a, b) => a + ", " + b) + ", " + "a" + "b" + ", " + x["a"] + x["b"]);

            foreach (var i in foo)
            {
                Console.WriteLine(i);
            }

            while (true) { }
        }
    }
}
