using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Fighter
    {
        int Hp { get; set; }
        int Damage { get; set; }
        double CritChance { get; set; }

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
            Hp -= -damage;
            if (Hp < 0 )
            {
                Hp = 0;
            }
        }
    }
}
