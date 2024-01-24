namespace DungeonGame.Core
{
    public class Map
    {
        public int[] Size = [40, 20];
        public Player Player { get; set; } = new(new Position(), 100, 20, 10);
        public List<Monster> Monsters { get; set; } = [];
        public Door Door { get; set; } = new(new Position());
        public List<Item> Items { get; set; } = [];

        public bool DirectionCheck(Direction direction, Position position)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (position.X == 0)
                        return false;
                    break;
                case Direction.Right:
                    if (position.X == Size[0] - 1)
                        return false;
                    break;
                case Direction.Up:
                    if (position.Y == 0)
                        return false;
                    break;
                case Direction.Down:
                    if (position.Y == Size[1] - 1)
                        return false;
                    break;
            }
            return true;
        }

        public int OnMonster(Position position)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.OnSameSpot(position))
                {
                    return monster.Id;
                }
            }
            return -1;
        }

        public int OnItem(Position position)
        {
            foreach (var item in Items)
            {
                if (item.OnSameSpot(position))
                {
                    return item.Id;
                }
            }
            return -1;
        }
    }
}
