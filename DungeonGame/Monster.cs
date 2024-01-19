using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Monster(int _id,  string _symbol,int[] _Position, int _Hp, int _Damage, int _CritChance) : Fighter(_Position,_Hp,_Damage,_CritChance)
    {
        public string Symbol { get; } = _symbol;
        public int id { get; } = _id;

        public static Monster CreateMonster(int id, int type,int[] position)
        {
            switch(type)
            {
                case 0:
                    return new Monster(id, "§", position, 50, 4, 10);
                case 1:
                    return new Monster(id, "$", position, 100, 2, 20);
                default:
                    return new Monster(id, "#", position, 30, 10, 40);
            }
        }
    }
}