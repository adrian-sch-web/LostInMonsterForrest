using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Game
    {
        public string Messages = "";
        public int Floor = 0;
        public int[] BoardSize = [40, 20]; 
        public Player Player { get; set; } = new([0, 0], 100, 20, 10);
        public List<Monster> Monsters { get; set; } = [];
        public Door Door { get; set; } = new([0, 0]);
        public Item Item { get; set; } = new([0, 0], " ");
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;
        public string FightMessage = "";
        private readonly Random random = new();
        
        public void Press(ConsoleKey key)
        {
            Messages = "";
            if (FightMode == -1)
            {
                if(key == ConsoleKey.Enter)
                {
                    return;
                }
                Direction direction = KeyToDirection(key);
                
                if (!DirectionCheck(direction, Player.Position))
                {
                    return;
                }
                Player.Move(direction);
                FightMode = MonsterOverlapCheck(Player.Position);
                if (FightMode != -1) return;
                
                if (Door.OnSameSpot(Player.Position))
                {
                    NextFloor();
                    return;
                }
                if (Item.OnSameSpot(Player.Position))
                {
                    if (Item.Type != " ")
                    {
                        Messages = "You collected a " + Item.Fullname() + "!";
                    }
                    Player.Collect(Item);
                    
                }
                MonsterTurn();
            }
            else
            {
                if(key != ConsoleKey.Enter)
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
            ItemSetup();
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
                int[] newPosition = GetNewPosition(monster.Position, direction);
                
                if (MonsterOverlapCheck(newPosition) != -1 || Door.OnSameSpot(newPosition) || Item.OnSameSpot(newPosition))
                {
                    continue;
                }
                monster.Move(direction);
            }
            FightMode = MonsterOverlapCheck(Player.Position);
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

        public bool DirectionCheck(Direction direction, int[] position)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (position[0] == 0)
                        return false;
                    break;
                case Direction.Right:
                    if (position[0] == BoardSize[0] - 1)
                        return false;
                    break;
                case Direction.Up:
                    if (position[1] == 0)
                        return false;
                    break;
                case Direction.Down:
                    if (position[1] == BoardSize[1] - 1)
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

        public void ItemSetup()
        {
            Item.Position = GetRandomPosition();
            switch (random.Next(3))
            {
                case 0: 
                    Item.Type = "C";
                    break;
                case 1:
                    Item.Type = "D";
                    break;
                case 2:
                    Item.Type = "H";
                    break;
            }
        }

        public int[] GetRandomPosition()
        {
            int xPos, yPos;
            do
            {
                xPos = random.Next(BoardSize[0]);
                yPos = random.Next(BoardSize[1]);
            } while (Player.OnSameSpot([xPos, yPos]) || MonsterOverlapCheck([xPos, yPos]) != -1 || Door.OnSameSpot([xPos,yPos]));
            return [xPos, yPos];
        }
        private void MonsterSetup(int amount)
        {
            Monsters = [];
            for(int i = 0; i < amount; i++)
            {
                int[] position = GetRandomPosition(); 
                Monsters.Add(Monster.CreateMonster(i + 1 , random.Next(3), position));
            }
        }

        public int MonsterOverlapCheck(int[] position)
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

        public int[] GetNewPosition(int[] position, Direction direction)
        {
            switch (direction)
            {
                case (Direction.Left):
                    return [position[0] - 1, position[1]];
                case (Direction.Right):
                    return [position[0] + 1, position[1]];
                case (Direction.Up):
                    return [position[0], position[1] - 1];
                default:
                    return [position[0], position[1] + 1];
            }
        }

        private Direction KeyToDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                default:
                    return Direction.Down;
            }
        }
    }
}
