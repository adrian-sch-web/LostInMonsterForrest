using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Player(int[] _Position, int _Hp, int _Damage, int _CritChance) : Fighter(_Position, _Hp, _Damage, _CritChance)
    {
        public void Collect(Item item)
        {
            switch(item.Type)
            {
                case "C":
                    CritChance += 5;
                    item.Type = " ";
                    break;
                case "D":
                    Damage += 5;
                    item.Type = " ";
                    break;
                case "H":
                    Hp += 5;
                    item.Type = " ";
                    break;
            }
        }
    }
}
