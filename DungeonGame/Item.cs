using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Item(int[] _position, string _type): Placeable(_position)
    {
        public string Type = _type;

        public string Fullname()
        {
            switch (Type)
            {
                case "C":
                    return "Critical Strike Chance Up"; 
                case "D":
                    return "Damage Up";
                case "H":
                    return "Heal";
                default:
                    return "";
            }
        }
    }
}
