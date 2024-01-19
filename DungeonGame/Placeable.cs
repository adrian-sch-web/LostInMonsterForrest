using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Placeable(int[] _position)
    {
        public int[] Position { get; set; } = _position;
    }
}
