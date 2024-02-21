namespace DungeonGame.Core
{
    public class Map
    {
        public static Position Size { get; } = new(20, 20);
        public Player Player { get; set; } = new(new Position(), 100, 100, 20, 20, 1);
        public Door Door { get; set; } = new(new Position());
        public List<Monster> Monsters { get; set; } = [];
        public List<Item> Items { get; set; } = [];
        public FloorType[,] Board { get; set; } = new FloorType[20, 20];

        private readonly Random random = new();
        private readonly List<FloorType[,]> floorFormations = Floor.GetBoards();

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
            return Board[position.X, position.Y] == FloorType.Tree;
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
            FloorSetup();
            DoorSetup();
            ItemSetup(1);
            MonsterSetup(1 + floor * 2);
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

        public void FloorSetup()
        {
            if (random.Next(3) == 0)
            {
                Board = floorFormations[0];
            }
            Board = floorFormations[random.Next(1, floorFormations.Count)];
            if (Board[Player.Position.X, Player.Position.Y] == FloorType.Tree || Board[Player.Position.X, Player.Position.Y] == FloorType.Mud)
            {
                Board = floorFormations[0];
            }
        }

        public double MoveCost(Position position)
        {
            return MoveCost(Board[position.X, position.Y]);
        }

        public double MoveCost(FloorType floor)
        {
            return floor switch
            {
                FloorType.Road => 0.5,
                FloorType.Mud => 2.0,
                FloorType.Normal => 1.0,
                _ => 99
            };
        }


        public Direction FindPath(Position position, Position destination)
        {
            double[,] snapShot = new double[Board.GetLength(0), Board.GetLength(1)];
            for (int i = 0; i < snapShot.GetLength(0); i++)
            {
                for (int j = 0; j < snapShot.GetLength(1); j++)
                {
                    snapShot[i, j] = MoveCost(Board[i, j]);
                }
            }
            snapShot[Door.Position.X, Door.Position.Y] = 99;
            Monsters.ForEach(monster => snapShot[monster.Position.X, monster.Position.Y] = 99);
            Items.ForEach(item => snapShot[item.Position.X, item.Position.Y] = 99);

            List<Path> openPaths = [new Path(position, [], 0, position.Distance(destination) / 2.0)];
            List<Path> closedPaths = [];

            while (openPaths.Count > 0)
            {
                var temp = openPaths.Last();
                if (temp.Position.SamePosition(destination))
                {
                    if (temp.FullWay.Count > 0)
                    {
                        var moveDirection = temp.FullWay.First();
                        if (snapShot[position.GetNeighbourPosition(moveDirection).X, position.GetNeighbourPosition(moveDirection).Y] >= 10)
                        {
                            return Direction.Idle;
                        }
                        return moveDirection;
                    }
                    return Direction.Idle;
                }
                closedPaths.Add(temp);
                openPaths.Remove(temp);
                var neighbours = temp.Neighbours(destination, snapShot[temp.Position.X, temp.Position.Y]);

                foreach (var newPath in neighbours)
                {
                    if (closedPaths.Any(path => path.Position.SamePosition(newPath.Position)))
                    {
                        continue;
                    }
                    int samePathIndex = openPaths.FindIndex(x => x.Position.SamePosition(newPath.Position));

                    if (samePathIndex > 0)
                    {
                        if (newPath.DistanceTraveled > openPaths[samePathIndex].DistanceTraveled)
                        {
                            continue;
                        }
                        openPaths.RemoveAt(samePathIndex);
                    }

                    int index = openPaths.FindIndex(x => x.Compare(newPath));
                    if (index < 0) index = openPaths.Count;
                    openPaths.Insert(index, newPath);
                }
            }
            return Direction.Idle;
        }

        private class Path(Position position, List<Direction> directions, double traveled, double distance)
        {
            public List<Direction> FullWay { get; set; } = directions;
            public Position Position { get; set; } = position;
            public double DistanceTraveled { get; set; } = traveled;
            public double ApproximateDistance { get; set; } = distance;

            public List<Path> Neighbours(Position destination, double moveCost)
            {
                List<Path> neighbours = [];
                var left = Position.GetNeighbourPosition(Direction.Left);
                var right = Position.GetNeighbourPosition(Direction.Right);
                var up = Position.GetNeighbourPosition(Direction.Up);
                var down = Position.GetNeighbourPosition(Direction.Down);

                if (left.InField(Size)) neighbours.Add(
                    new(left, [.. FullWay, Direction.Left], DistanceTraveled + moveCost, left.Distance(destination) / 2.0)
                    );
                if (right.InField(Size)) neighbours.Add(
                    new(right, [.. FullWay, Direction.Right], DistanceTraveled + moveCost, right.Distance(destination) / 2.0)
                    );
                if (up.InField(Size)) neighbours.Add(
                    new(up, [.. FullWay, Direction.Up], DistanceTraveled + moveCost, up.Distance(destination) / 2.0)
                    );
                if (down.InField(Size)) neighbours.Add(
                    new(down, [.. FullWay, Direction.Down], DistanceTraveled + moveCost, down.Distance(destination) / 2.0)
                    );

                return neighbours;
            }

            public bool Compare(Path other)
            {
                if (ApproximateDistance + DistanceTraveled < other.ApproximateDistance + other.DistanceTraveled)
                {
                    return true;
                }
                if (ApproximateDistance + DistanceTraveled == other.ApproximateDistance + other.DistanceTraveled)
                {
                    return DistanceTraveled > other.DistanceTraveled;
                }
                return false;
            }
        }
    }
}
