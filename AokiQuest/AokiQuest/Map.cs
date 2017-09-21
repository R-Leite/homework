using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AokiQuest
{
    class Map
    {
        #region
        public Point MinPoint { get; }
        public Point MaxPoint { get; }
        #endregion

        public Map()
        {
            MinPoint = new Point(0, 0);
            MaxPoint = new Point(10, 10);
        }
    }
}
