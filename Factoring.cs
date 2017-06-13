using System.Collections.Generic;
using System;
using System.Linq;

public class Hello{
    public static void Main(){
        // Here your code !
        
        var number = 24;
		var factors = new List<int>();

		while (true)
        {
			int i = 0;
			for (i = 2; i < number; i++)
			{
                if (number % i == 0) { factors.Add(i); number /= i; break; }
			}
			if (i == number) { factors.Add(i); break; }
        }

		var num = 24;
        var list = Enumerable.Range(2, num - 2).Where(x => num % x == 0);
        MessageBox.Show(list.Min().ToString());
        MessageBox.Show(list.Max().ToString());
        var fib = Enumerable.Repeat(new[] { 2, num }, 2).Select(x => x[1] = x[1] / (x[0] = Enumerable.Range(2, x[1] - 2).Where(_ => x[1] % _ == 0).Min()));
        MessageBox.Show(fib.Select(x => x.ToString()).Aggregate((a, b) => a + "," + b));

        Console.WriteLine(string.Join(",", factors));
    }
}
