namespace DungeonGame.Core
{
    public class Map
    {
        public int[] Size = [40, 20];
        public Player Player { get; set; } = new(new Position(), 100, 20, 10);
        public List<Monster> Monsters { get; set; } = [];
        public Door Door { get; set; } = new(new Position());
        public List<Item> Items { get; set; } = [];

    }
}
