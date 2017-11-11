using System;
using System.IO;
using System.Linq;

namespace Q6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileName = "Q6_in.txt";
                var file = File.ReadLines(fileName);

                // 個数
                var count = int.Parse(file.FirstOrDefault());
                var pointList = file.Skip(1).Take(count).Select(x => { var p = x.Split(' '); return new Point(int.Parse(p[0]), int.Parse(p[1])); });
                var maxDistance = pointList.SelectMany((val, idx) => pointList.Skip(idx).Select(y => y.getDistance(val))).Max();

                Console.WriteLine($"最大線分長:{maxDistance.ToString("F3")}");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("終了するには何かキーを押してください...");
                Console.ReadKey();
            }
        }
    }
}
