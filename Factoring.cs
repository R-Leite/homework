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

		var aaa = Enumerable.Range(2, number + 1).Where(x => number % x == 0);
//        aaa.Where(x=>Enumerable.Range(2, x).Where(y=>x%y==0)
        Console.WriteLine(string.Join(",", factors));
    }
}
