using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            var startDate = DateTime.Parse("2000/01/01");
            var endDate = DateTime.Parse("2014/01/01");
            var day = DayOfWeek.Friday;
            var date = 13;

            Console.WriteLine(GetDayOfDate(startDate, endDate, day, date).Select(x => x.ToShortDateString()).Aggregate((s, n) => s + Environment.NewLine + n));

            Console.WriteLine("終了するには何かキーを押してください...");
            Console.ReadKey();
        }

        static IEnumerable<DateTime> GetDayOfDate(DateTime start, DateTime end, DayOfWeek day, int date)
        {
            return Enumerable.Range(0, int.MaxValue).Select(x => start.AddDays(x)).TakeWhile(d => d < end).Where(d => d.Day == date).Where(d => d.DayOfWeek == day);
        }
    }
}
