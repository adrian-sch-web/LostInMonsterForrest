﻿namespace DungeonGame.Core
{
    public class Game
    {
        public string Messages = "";
        public int Floor = 0;
        public int[] BoardSize = [40, 20]; 
        public Player Player { get; set; } = new(new Position(), 100, 20, 10);
        public List<Monster> Monsters { get; set; } = [];
        public Door Door { get; set; } = new(new Position());
        public List<Item> Items { get; set; } = [];
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;
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
                
                if (!DirectionCheck(direction, Player.Position))
                {
                    return;
                }
                Player.Move(direction);
                FightMode = OverlappingMonster(Player.Position);
                if (FightMode != -1) return;
                
                if (Door.OnSameSpot(Player.Position))
                {
                    NextFloor();
                    return;
                }

                Item? item = Items.Find(x => x.Id == OnItem(Player.Position));
                if (item != null)
                {
                    Player.Collect(item);
                    Items.Remove(item);
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
                if(Player.Hp == 0)
                {
                    IsRunning = false;
                }
            }
        }

        public void Setup()
        {
            MonsterSetup(3 + 2 * Floor);
            Door.Position = GetRandomPosition();
            ItemSetup(1);
        }

        public void MonsterTurn()
        {
            foreach (var monster in Monsters)
            {
                Direction direction;
                do
                {
                    direction = (Direction)random.Next(4);
                } while (!DirectionCheck(direction, monster.Position));
                Position newPosition = GetNewPosition(monster.Position, direction);
                
                if (OverlappingMonster(newPosition) != -1 || Door.OnSameSpot(newPosition) || OnItem(newPosition) != -1)
                {
                    continue;
                }
                monster.Move(direction);
            }
            FightMode = OverlappingMonster(Player.Position);
        }

        public bool Fight()
        {
            Monster? monster = Monsters.Find(x => x.Id == FightMode);
            if(monster == null)
            {
                return false;
            }
            if(monster.Hp == 0)
            {
                FightMode = -1;
                Monsters.Remove(monster);
                return false;
            }
            int attack = Player.Attack(random.Next(100));
            monster.Defend(attack);
            Messages += "You striked the monster!\n";
            if (attack > Player.Damage)
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
            Player.Defend(attack);
            Messages += "The monster striked you!\n";
            if (attack > monster.Damage)
            {
                Messages += "It was a critical strike!\n";
            }
            Messages += "You lost " + attack + " hp\n\n";

            return Player.Hp > 0;
        }

        public bool DirectionCheck(Direction direction, Position position)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (position.X == 0)
                        return false;
                    break;
                case Direction.Right:
                    if (position.X == BoardSize[0] - 1)
                        return false;
                    break;
                case Direction.Up:
                    if (position.Y == 0)
                        return false;
                    break;
                case Direction.Down:
                    if (position.Y == BoardSize[1] - 1)
                        return false;
                    break;
            }
            return true;
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
                xPos = random.Next(BoardSize[0]);
                yPos = random.Next(BoardSize[1]);
                newPosition = new Position(xPos, yPos);
            } while (Player.OnSameSpot(newPosition) || OverlappingMonster(new Position(0, 0)) != -1 || Door.OnSameSpot(newPosition));
            return newPosition;
        }

        private void MonsterSetup(int amount)
        {
            Monsters = [];
            for(int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition(); 
                Monsters.Add(Monster.CreateMonster(i + 1 , (MonsterType) random.Next(3), position));
            }
        }

        public void ItemSetup(int amount)
        {
            Items = [];
            for(int i = 0; i < amount; i++)
            {
                Position position = GetRandomPosition();
                Items.Add(Item.CreateItem(i + 1, (ItemType)random.Next(3), position));
            }

        }

        public int OverlappingMonster(Position position)
        {
            foreach (Monster monster in Monsters)
            {
                if(monster.OnSameSpot(position))
                {
                    return monster.Id;
                }
            }
            return -1;
        }

        public int OnItem(Position position)
        {
            foreach(var item in Items)
            {
                if (item.OnSameSpot(position))
                { 
                    return item.Id;
                }
            }
            return -1;
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
