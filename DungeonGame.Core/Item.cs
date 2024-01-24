﻿namespace DungeonGame.Core
{
    public class Item(int _id, ItemType _type, int[] _position) : Placeable(_position)
    {
        public int Id = _id;
        public ItemType Type = _type;

        public string Fullname()
        {
            switch (Type)
            {
                case ItemType.Crit:
                    return "Critical Strike Chance Up"; 
                case ItemType.Damage:
                    return "Damage Up";
                case ItemType.Heal:
                    return "Heal";
                default:
                    return "";
            }
        }

        public static Item CreateItem(int id,ItemType type, int[] position )
        {
            return new Item(id, type, position);
        }
    }

    public enum ItemType
    {
        Crit,
        Damage,
        Heal
    }
}
