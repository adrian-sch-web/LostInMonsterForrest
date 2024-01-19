using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Fighter(int[] _Position, int _Hp, int _Damage, int _CritChance): Placeable(_Position)
    {
        public int Hp { get; set; } = _Hp;
        public int Damage { get; set; } = _Damage;
        public int CritChance { get; set; } = _CritChance;

        public int Attack(int critRoll)
        {
            if (100 - CritChance < critRoll)
            {
                return Damage * 2;
            }
            return Damage;
        }

        public void Defend(int damage)
        {
            Hp -= damage;
            if (Hp < 0 )
            {
                Hp = 0;
            }
        }

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
