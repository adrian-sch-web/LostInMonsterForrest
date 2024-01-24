using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame.Core
{
    public class Position
    {
        public int X;
        public int Y;
        public Position(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
        public Position()
        {
            X = 0;
            Y = 0;
        }
    }
}
