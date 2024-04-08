namespace DungeonGame.Core
{
    public class Item(int _id, ItemType _type, Position _position) : Placeable(_position)
    {
        public int Id = _id;
        public ItemType Type = _type;

        public static Item CreateItem(int id,ItemType type, Position position )
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
