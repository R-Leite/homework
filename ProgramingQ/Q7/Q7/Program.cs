using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q7
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> queue = new Queue<string>();

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                if(currentNode.IsGoal)
                {
                    break;
                }
                foreach(var pos in currentNode.NextPositions())
                {
                    var next = currentNode.Clone().ToString();
                    next.Visited = true;
                    queue.Enqueue(next);
                }
            }
        }
    }
}
