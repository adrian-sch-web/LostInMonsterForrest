namespace DungeonGame.Core
{
    public class Map
    {
        public static Position Size { get; } = new(20, 20);
        public Player Player { get; set; } = new(new Position(), 100, 20, 20);
        public Door Door { get; set; } = new(new Position());
        public List<Tree> Trees { get; set; } = [];
        public List<Monster> Monsters { get; set; } = [];
        public List<Item> Items { get; set; } = [];

        private readonly Random random = new();
        private readonly List<Position>[] treeFormations = Tree.GetTreeFormations();

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

        public bool IsEmptyField(Position position)
        {
            return !Player.OnSameSpot(position)
                && OnMonster(position) == -1
                && !Door.OnSameSpot(position)
                && !OnTree(position)
                && OnItem(position) == -1;
        }
        public void Setup(int floor)
        {
            TreeSetup();
            DoorSetup();
            ItemSetup(1);
            MonsterSetup(3 + floor * 2);
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
            } while (!IsEmptyField(newPosition));
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
            List<Position> formation = treeFormations[random.Next(treeFormations.Length)];
            for (int i = 0; i < formation.Count; i++)
            {
                if (Player.OnSameSpot(formation[i]))
                {
                    return;
                }
            }
            for (int i = 0; i < formation.Count; i++)
            {
                Trees.Add(new(formation[i]));
            }

        }

        public Direction FindPath(Position position, Position destination)
        {
            bool[,] snapShot = new bool[Size.X, Size.Y];

            snapShot[Door.Position.X, Door.Position.Y] = true;
            Monsters.ForEach(monster => snapShot[monster.Position.X, monster.Position.Y] = true);
            Trees.ForEach(tree => snapShot[tree.Position.X, tree.Position.Y] = true);
            Items.ForEach(item => snapShot[item.Position.X, item.Position.Y] = true);

            List<Path> openPaths = [];
            var left = position.GetNeighbourPosition(Direction.Left);
            var right = position.GetNeighbourPosition(Direction.Right);
            var up = position.GetNeighbourPosition(Direction.Up);
            var down = position.GetNeighbourPosition(Direction.Down);

            snapShot[position.X, position.Y] = true;
            if (left.InField(Size) && !snapShot[left.X, left.Y])
                openPaths.Add(new(left, Direction.Left, 1, left.Distance(destination)));
            if (right.InField(Size) && !snapShot[right.X, right.Y])
                openPaths.Add(new(right, Direction.Right, 1, right.Distance(destination)));
            if (up.InField(Size) && !snapShot[up.X, up.Y])
                openPaths.Add(new(up, Direction.Up, 1, up.Distance(destination)));
            if (down.InField(Size) && !snapShot[down.X, down.Y])
                openPaths.Add(new(down, Direction.Down, 1, down.Distance(destination)));

            openPaths.Sort((pathA, pathB) => pathB.SumTraveledApproximate - pathA.SumTraveledApproximate);

            foreach (var openPath in openPaths)
            {
                snapShot[openPath.Position.X, openPath.Position.Y] = true;
            }
            while (openPaths.Count > 0)
            {
                var temp = openPaths.Last();
                if (temp.Position.SamePosition(destination))
                    return temp.FirstDirection;
                openPaths.Remove(temp);
                var neighbours = temp.Neighbours();

                foreach (var neighbour in neighbours)
                {
                    if (snapShot[neighbour.X, neighbour.Y])
                        continue;
                    snapShot[neighbour.X, neighbour.Y] = true;
                    var newPath = new Path(neighbour, temp.FirstDirection, temp.DistanceTraveled + 1, neighbour.Distance(destination));
                    int index = openPaths.FindIndex(x => x.SumTraveledApproximate < newPath.SumTraveledApproximate);
                    if (index < 0) index = openPaths.Count;
                    openPaths.Insert(index, newPath);
                }
            }
            return Direction.Idle;
        }

        private class Path(Position position, Direction firstDirection, int traveled, int distance)
        {
            public Direction FirstDirection { get; set; } = firstDirection;
            public Position Position { get; set; } = position;
            public int DistanceTraveled { get; set; } = traveled;
            public int SumTraveledApproximate { get; set; } = distance + traveled;

            public List<Position> Neighbours()
            {
                List<Position> neighbours = [];
                var left = Position.GetNeighbourPosition(Direction.Left);
                var right = Position.GetNeighbourPosition(Direction.Right);
                var up = Position.GetNeighbourPosition(Direction.Up);
                var down = Position.GetNeighbourPosition(Direction.Down);

                if (left.InField(Size)) neighbours.Add(left);
                if (right.InField(Size)) neighbours.Add(right);
                if (up.InField(Size)) neighbours.Add(up);
                if (down.InField(Size)) neighbours.Add(down);

                return neighbours;
            }
        }
    }
}
