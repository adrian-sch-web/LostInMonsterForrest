using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal interface IPlaceable
    {
        int[] Position { get { return Position; } }
    }
}
