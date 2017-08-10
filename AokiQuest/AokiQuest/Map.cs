using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AokiQuest
{
    class Map
    {
        private int max_x;
        private int max_y;

        public Map()
        {
            max_x = 10;
            max_y = 10;
        }

        public void Render()
        {
            while(true)
            {
                var map = new List<string>();
                for (var y = 0; y < max_y; y++)
                {
                    for (var x = 0; x < max_x; x++)
                    {
                        if (y == 0 || y == max_y - 1)
                        {
                            map.Add("-");
                        }
                        else if (x == 0 || x == max_x - 1)
                        {
                            map.Add("|");
                        }
                        else
                        {
                            map.Add(" ");
                        }
                    }
                    map.Add("\n");
                }

                var m = map.Aggregate((a, b) => a + b);

                Console.WriteLine(m);
                Console.Clear();
            }
        }
    }
}
