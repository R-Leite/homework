using System.Collections.Generic;
using System;
using System.Linq;

public class Hello{
    public static void Main(){
		var fib = Enumerable.Repeat(new[] { 1L, 0L }, 10).Select(x => x[1] = (x[0] = x[0] + x[1]) - x[1]);
		Console.WriteLine(fib.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b));
	}
}
