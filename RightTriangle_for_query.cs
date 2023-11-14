using System;
using System.Collections.Generic;
using System.Linq;

public class Hello{
    public static void Main(){
        // クエリバージョン
		var ans =
			from x in Enumerable.Range(1, 10)
            from y in Enumerable.Range(1, x)
            from z in Enumerable.Range(1, y)
            where (x + y + z == 24)
            where (x * x == y * y + z * z)
            select new List<string>() { x.ToString(), y.ToString(), z.ToString() };

            var ansString = ans.Select(_ => _.Aggregate((a, b) => a + ", " + b));
            foreach (var i in ansString) { Console.WriteLine(i); }
    }
}
