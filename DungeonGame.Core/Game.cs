namespace DungeonGame.Core
{
    public class Game
    {
        public string Messages = "";
        public int Floor = 0;
        public Map Map = new Map();
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;        //Index of monster in fight -1 if not in fight
        public string FightMessage = "";
        private readonly Random random = new();
        
        public void Press(Input input)
        {
            Messages = "";
            if (FightMode == -1)
            {
                if(input == Input.Enter)
                {
                    return;
                }

                Direction direction = InputToDirection(input);
                
                if (!Map.DirectionCheck(direction, Map.Player.Position))
                {
                    return;
                }
                Map.Player.Move(direction);

                CollisionChecks();
                if(FightMode != -1)
                {
                    return;
                }

                MonsterTurn();
            }
            else
            {
                if(input != Input.Enter)
                {
                    return;
                }
                if (Fight())
                {
                    return;
                }
                if(Map.Player.Hp == 0)
                {
                    IsRunning = false;
                }
            }
        }

        public void CollisionChecks()
        {
            FightMode = Map.OnMonster(Map.Player.Position);
            if (FightMode != -1)
            {
                return;
            }

            Item? item = Map.Items.Find(x => x.Id == Map.OnItem(Map.Player.Position));
            if (item != null)
            {
                Map.Player.Collect(item);
                Map.Items.Remove(item);
                return;
            }

            if (Map.Door.OnSameSpot(Map.Player.Position))
            {
                NextFloor();
            }
        }

        public void Setup()
        {
            MonsterSetup(3 + 2 * Floor);
            DoorSetup();
            ItemSetup(1);
        }

        public void MonsterTurn()
        {
            foreach (var monster in Map.Monsters)
            {
                Direction direction;
                do
                {
                    direction = (Direction)random.Next(4);
                } while (!Map.DirectionCheck(direction, monster.Position));
                Position newPosition = GetNewPosition(monster.Position, direction);
                
                if (Map.OnMonster(newPosition) != -1 || Map.Door.OnSameSpot(newPosition) || Map.OnItem(newPosition) != -1)
                {
                    continue;
                }
                monster.Move(direction);
            }
            FightMode = Map.OnMonster(Map.Player.Position);
        }

        public bool Fight()
        {
            Monster? monster = Map.Monsters.Find(x => x.Id == FightMode);
            if(monster == null)
            {
                return false;
            }
            if(monster.Hp == 0)
            {
                FightMode = -1;
                Map.Monsters.Remove(monster);
                return false;
            }
            int attack = Map.Player.Attack(random.Next(100));
            monster.Defend(attack);
            Messages += "You striked the monster!\n";
            if (attack > Map.Player.Damage)
            {
                Messages += "It was a critical strike!\n";
            }
            Messages += "The monster lost " + attack + " hp\n\n";
            if (monster.Hp <= 0)
            {
                Messages += "The monster died.";
                return false;
            }
            attack = monster.Attack(random.Next(100));
            Map.Player.Defend(attack);
            Messages += "The monster striked you!\n";
            if (attack > monster.Damage)
            {
                Messages += "It was a critical strike!\n";
            }
            Messages += "You lost " + attack + " hp\n\n";

            return Map.Player.Hp > 0;
        }

        public void NextFloor()
        {
            Floor++;
            Setup();
            Messages = "You have reached Floor " + Floor;
        }

        public Position GetRandomPosition()
        {
            int xPos, yPos;
            Position newPosition;
            do
            {
                xPos = random.Next(Map.Size[0]);
                yPos = random.Next(Map.Size[1]);
                newPosition = new Position(xPos, yPos);
            } while (Map.Player.OnSameSpot(newPosition) || Map.OnMonster(new Position(0, 0)) != -1 || Map.Door.OnSameSpot(newPosition));
            return newPosition;
        }

        private void MonsterSetup(int amount)
        {
            Map.Monsters = [];
            for(int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Map.Monsters.Add(Monster.CreateMonster(i + 1 , (MonsterType) random.Next(3), position));
            }
        }

        public void ItemSetup(int amount)
        {
            Map.Items = [];
            for(int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Map.Items.Add(Item.CreateItem(i + 1, (ItemType)random.Next(3), position));
            }

        }

        public void DoorSetup()
        {
            Map.Door.Position = GetRandomPosition();
        }

        public Position GetNewPosition(Position position, Direction direction)
        {
            switch (direction)
            {
                case (Direction.Left):
                    return new Position(position.X - 1, position.Y);
                case (Direction.Right):
                    return new Position(position.X + 1, position.Y);
                case (Direction.Up):
                    return new Position(position.X, position.Y - 1);
                default:
                    return new Position(position.X, position.Y + 1);
            }
        }

        private Direction InputToDirection(Input input)
        {
            switch (input)
            {
                case Input.Left:
                    return Direction.Left;
                case Input.Right:
                    return Direction.Right;
                case Input.Up:
                    return Direction.Up;
                default:
                    return Direction.Down;
            }
        }
    }
}
