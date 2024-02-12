namespace DungeonGame.Core
{
    public class Map
    {
        public Position Size = new(20, 20);
        public Player Player { get; set; } = new(new Position(), 100, 20, 20);
        public Door Door { get; set; } = new(new Position());
        public List<Tree> Trees { get; set; } = [];
        public List<Monster> Monsters { get; set; } = [];
        public List<Item> Items { get; set; } = [];

        private readonly Random random = new();
        private readonly Position[][] treeFormations = Tree.GetTreeFormations();

  
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

        public bool OnTree(Position position)
        {
            foreach (Placeable tree in Trees)
            {
                if (tree.OnSameSpot(position))
                {
                    return true;
                }
            }
            return false;
        }

        public void Setup(int floor)
        {
            TreeSetup();
            DoorSetup();
            ItemSetup(1);
            MonsterSetup(3 + 2 * floor);
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
            } while (Player.OnSameSpot(newPosition) || OnMonster(newPosition) != -1 || Door.OnSameSpot(newPosition) || OnTree(newPosition));
            return newPosition;
        }

        private void MonsterSetup(int amount)
        {
            Monsters = [];
            for (int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Monsters.Add(Monster.CreateMonster(i + 1, (MonsterType)random.Next(3), position, GetRandomPosition()));
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

        public void TreeSetup()
        {
            Trees.Clear();
            if (random.Next(3) == 0)
            {
                return;
            }
            Position[] formation = treeFormations[random.Next(treeFormations.Length)];
            for (int i = 0; i < formation.Length; i++)
            {
                if (Player.OnSameSpot(formation[i]))
                {
                    return;
                }
            }
            for (int i = 0; i < formation.Length; i++)
            {
                Trees.Add(new(formation[i]));
            }

        }
    }
}
