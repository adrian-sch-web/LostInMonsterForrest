using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Fighter(int[] _Position, int _Hp, int _Damage, int _CritChance): Placeable(_Position)
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
    }
}
