using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Monster(int _id,  string _symbol,int[] _position, int _Hp, int _Damage, int _CritChance) : Fighter, IPlaceable
    {
        public string Symbol { get; } = _symbol;
        public int id { get; } = _id;
        public int[] Position { get; } = _position;
        public int Hp { get; } = _Hp;
        public int Damage { get; } = _Damage;
        public int CritChance {  get; } = _CritChance;

        public static Monster CreateMonster(int id, int type,int[] position)
        {
            switch(type)
            {
                case 0:
                    return new Monster(id, "§", position, 50, 4, 10);
                case 1:
                    return new Monster(id, "$", position, 100, 2, 30);
                default:
                    return new Monster(id, "#", position, 30, 10, 60);
            }
        }
    }
}