using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Door(int[] _position): IPlaceable
    {
        public int[] Position = _position;
    }
}
