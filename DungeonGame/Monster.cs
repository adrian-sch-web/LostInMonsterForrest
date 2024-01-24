using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Monster(int _id,  int _Type,int[] _Position, int _Hp, int _Damage, int _CritChance) : Fighter(_Position,_Hp,_Damage,_CritChance)
    {
        public int Type { get; } = _Type;
        public int Id { get; } = _id;

        public static Monster CreateMonster(int id, int type,int[] position)
        {
            switch(type)
            {
                case 0:
                    return new Monster(id, type, position, 50, 4, 10);
                case 1:
                    return new Monster(id, type, position, 100, 2, 20);
                default:
                    return new Monster(id, type, position, 30, 10, 40);
            }
        }
    }
}