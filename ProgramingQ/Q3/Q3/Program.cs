using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            var startDate = DateTime.Parse("2000/01/01");
            var endDate = DateTime.Parse("2014/01/01");

            Console.WriteLine(GetFridayThe13th(startDate, endDate).Select(x => x.ToShortDateString()).Aggregate((s, n) => s + Environment.NewLine + n));

            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }

        static IEnumerable<DateTime> GetFridayThe13th(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, int.MaxValue).Select(x => start.AddDays(x)).TakeWhile(d => d < end).Where(d => d.Day == 13).Where(d => d.DayOfWeek == DayOfWeek.Friday);
        }
    }
}
