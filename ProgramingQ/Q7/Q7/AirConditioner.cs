using System;
using System.Collections.Generic;
using System.Linq;

namespace Q7
{
    class AirConditioner
    {
        private IEnumerable<int> _process;
        private int _currentTemperature;

        private const int MAX = 35;
        private const int MIN = 15;

        public static int SettingTemperature;

        static IEnumerable<int> operateList;
        static IEnumerable<int> already;

        public AirConditioner(IEnumerable<int> process, int temp)
        {
            _process = process;
            _currentTemperature = temp;
        }

        static AirConditioner()
        {
            operateList = new List<int>() { -10, 10, -5, 5, -1, 1 };
            already = new List<int>();
        }

        public IEnumerable<AirConditioner> GetChildren()
        {
            return operateList.Select(x => new AirConditioner(_process.Concat(Enumerable.Repeat(x, 1)), x + _currentTemperature)).Where(x => x._currentTemperature >= MIN).Where(x => x._currentTemperature <= MAX).Where(x => !already.Contains(x._currentTemperature));
        }

        public bool isGoal()
        {
            return _currentTemperature == SettingTemperature;
        }

        public void ShowResult()
        {
            Console.WriteLine($"最小操作手順：{_process.Count()}");
            Console.WriteLine($"操作手順：{_process.Select(x => x.ToString()).Aggregate((a, b) => a + " -> " + b)}");
        }
    }
}
