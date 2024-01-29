namespace DungeonGame.Core
{
    public class Map
    {
        public Position Size = new(40, 20);
        public Player Player { get; set; } = new(new Position(), 100, 20, 20);
        public Door Door { get; set; } = new(new Position());
        public List<Monster> Monsters { get; set; } = [];
        public List<Item> Items { get; set; } = [];

        private readonly Random random = new();

        public bool DirectionCheck(Direction direction, Position position)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (position.X == 0)
                        return false;
                    break;
                case Direction.Right:
                    if (position.X == Size.X - 1)
                        return false;
                    break;
                case Direction.Up:
                    if (position.Y == 0)
                        return false;
                    break;
                case Direction.Down:
                    if (position.Y == Size.Y - 1)
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
            foreach (Item item in Items)
            {
                if (item.OnSameSpot(position))
                {
                    return item.Id;
                }
            }
            return -1;
        }

        public void Setup(int floor)
        {
            MonsterSetup(3 + 2 * floor);
            DoorSetup();
            ItemSetup(1);
        }

        public Position GetRandomPosition()
        {
            int xPos, yPos;
            Position newPosition;
            do
            {
                xPos = random.Next(Size.X);
                yPos = random.Next(Size.Y);
                newPosition = new Position(xPos, yPos);
            } while (Player.OnSameSpot(newPosition) || OnMonster(newPosition) != -1 || Door.OnSameSpot(newPosition));
            return newPosition;
        }

        private void MonsterSetup(int amount)
        {
            Monsters = [];
            for (int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Monsters.Add(Monster.CreateMonster(i + 1, (MonsterType)random.Next(3), position));
            }
        }

        public void ItemSetup(int amount)
        {
            Items = [];
            for (int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Items.Add(Item.CreateItem(i + 1, (ItemType)random.Next(3), position));
            }

        }

        public void DoorSetup()
        {
            Door.Position = GetRandomPosition();
        }
    }
}
