using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Player(int[] _position, int _Hp, int _Damage, int _CritChance) : Fighter, IPlaceable
    {
        public int[] Position { get; } = _position;
        public int Hp { get; } = _Hp;
        public int Damage { get; } = _Damage;
        public int CritChance { get; } = _CritChance;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Position[0]--;
                    break;
                case Direction.Right:
                    Position[0]++;
                    break;
                case Direction.Up:
                    Position[1]--;
                    break;
                case Direction.Down:
                    Position[1]++;
                    break;
            }
        }
    }
}
