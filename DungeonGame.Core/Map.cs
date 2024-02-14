namespace DungeonGame.Core
{
    public class Map
    {
        public static Position Size { get; } = new(20, 20);
        public Player Player { get; set; } = new(new Position(), 100, 100, 20, 20);
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

            List<Path> openPaths = [new Path(position, [], 0, position.Distance(destination))];

            snapShot[openPaths.First().Position.X, openPaths.First().Position.Y] = true;
            while (openPaths.Count > 0)
            {
                var temp = openPaths.Last();
                if (temp.Position.SamePosition(destination))
                {
                    if (temp.FullWay.Count > 0)
                    {
                        return temp.FullWay.First();
                    }
                }
                openPaths.Remove(temp);
                var neighbours = temp.Neighbours(destination);

                foreach (var newPath in neighbours)
                {
                    if (snapShot[newPath.Position.X, newPath.Position.Y])
                    {
                        continue;
                    }
                    snapShot[newPath.Position.X, newPath.Position.Y] = true;
                    int index = openPaths.FindIndex(x => x.Compare(newPath));
                    if (index < 0) index = openPaths.Count;
                    openPaths.Insert(index, newPath);
                }
                //ShowAStar(snapShot, openPaths, destination);
            }
            return Direction.Idle;
        }

        private class Path(Position position, List<Direction> directions, int traveled, int distance)
        {
            public List<Direction> FullWay { get; set; } = directions;
            public Position Position { get; set; } = position;
            public int DistanceTraveled { get; set; } = traveled;
            public int SumTraveledApproximate { get; set; } = distance + traveled;

            public List<Path> Neighbours(Position destination)
            {
                List<Path> neighbours = [];
                var left = Position.GetNeighbourPosition(Direction.Left);
                var right = Position.GetNeighbourPosition(Direction.Right);
                var up = Position.GetNeighbourPosition(Direction.Up);
                var down = Position.GetNeighbourPosition(Direction.Down);

                if (left.InField(Size)) neighbours.Add(new(left, [.. FullWay, Direction.Left], DistanceTraveled + 1, left.Distance(destination)));
                if (right.InField(Size)) neighbours.Add(new(right, [.. FullWay, Direction.Right], DistanceTraveled + 1, right.Distance(destination)));
                if (up.InField(Size)) neighbours.Add(new(up, [.. FullWay, Direction.Up], DistanceTraveled + 1, up.Distance(destination)));
                if (down.InField(Size)) neighbours.Add(new(down, [.. FullWay, Direction.Down], DistanceTraveled + 1, down.Distance(destination)));

                return neighbours;
            }

            public bool Compare(Path other)
            {
                if (SumTraveledApproximate < other.SumTraveledApproximate)
                {
                    return true;
                }
                if (SumTraveledApproximate == other.SumTraveledApproximate)
                {
                    return DistanceTraveled > other.DistanceTraveled;
                }
                return false;
            }
        }

        private void ShowAStar(bool[,] snapShot, List<Path> openPaths, Position destination)
        {
            bool[,] snapShotCopy = new bool[Size.X, Size.Y];

            for (int i = 0; i < snapShot.GetLength(0); i++)
            {
                for (int j = 0; j < snapShot.GetLength(1); j++)
                {
                    snapShotCopy[i, j] = snapShot[i, j];
                }
            }

            Console.Clear();
            for (int i = 0; i < snapShot.GetLength(0); i++)
            {
                string line = "";
                for (int j = 0; j < snapShot.GetLength(1); j++)
                {
                    if (snapShot[j, i] && snapShotCopy[j, i])
                    {
                        if (openPaths.FindIndex(a => a.Position.SamePosition(new(j, i))) == -1)
                        {
                            if (Trees.FindIndex(a => a.Position.SamePosition(new(j, i))) != -1)
                            {
                                line += "T";
                            }
                            else
                            {
                                line += "■";
                            }
                        }
                        else
                        {
                            line += "+";
                        }
                    }
                    else if (snapShot[j, i])
                    {
                        line += "*";
                    }
                    else if (destination.X == j && destination.Y == i)
                    {
                        line += "G";
                    }
                    else
                    {
                        line += ".";
                    }
                }
                Console.WriteLine(line);
            }
            Thread.Sleep(100);
        }
    }
}
