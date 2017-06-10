using System.Collections.Generic;
using System.Linq;
using System;

public class Hello{
    public static void Main(){
		//
        var ans = Enumerable.Range(1, 10).SelectMany(x => Enumerable.Range(1, x).SelectMany(y => Enumerable.Range(1, y).Where(z => x + y + z == 24).Where(z => x * x == y * y + z * z).Select(z=>new List<int> { x, y, z }))).ToList();
        ans.ForEach(_=>_.ForEach(x=>Console.Write(x+" ")));
    }
}
