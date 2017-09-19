using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AokiQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var map = new Map();
            //map.Render();
            // 全てのプロセスを列挙する
            foreach (var p in System.Diagnostics.Process.GetProcessesByName("MftHmi"))
            {
                Console.WriteLine(p.Id + ":" + p.ProcessName);
                var app = p;
                Console.WriteLine(app.Handle);
            }
            Console.ReadLine();
        }
    }
}
