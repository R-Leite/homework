using System.Collections.Generic;
using System.Linq;
using System;

public class Hello{
    public static void Main(){
		//
		var ans = 
			Enumerable.Range(1, 10)
            .SelectMany(_ => Enumerable.Range(1, _), (a, b) => new { a, b })
            .SelectMany(_ => Enumerable.Range(1, _.b), (_, c) => new { _.a, _.b, c })
            .Where(_ => _.a + _.b + _.c == 24)
            .Where(_ => _.a * _.a == _.b * _.b + _.c * _.c)
            .Select(_ => new List<string> { _.a.ToString(), _.b.ToString(), _.c.ToString() });

		var ansString = ans.Select(_ => _.Aggregate((a, b) => a + ",  + b));
        foreach (var i in ansString) { Console.WriteLine(i); }
    }
}
