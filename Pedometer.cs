using System;

public class Hello{
    public static void Main(){
        // Here your code !
            var pedometer = Pedometer ();
            System.Console.WriteLine(pedometer());
            System.Console.WriteLine(pedometer());
            System.Console.WriteLine(pedometer());
    }
    
   public static Func<int> Pedometer ()
    {
        var count = 0;
        return () =>
        {
            return ++count;
        };
    }
}