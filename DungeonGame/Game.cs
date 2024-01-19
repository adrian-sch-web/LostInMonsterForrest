using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class Game
    {
        private int floor = 0;
        public int[] BoardSize = [40, 20]; 
        public Player Player { get; set; } = new Player([0, 0], 100, 20, 0);
        public List<Monster> Monsters { get; set; } = [];
        public Door Door { get; set; } = new Door([0, 0]);
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;
        private readonly Random random = new();
        
        public void Press(ConsoleKey key)
        {
            if (FightMode == -1)
            {
                if(key == ConsoleKey.Enter)
                {
                    return;
                }
                Direction direction;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        direction =  Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        direction =  Direction.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    default:
                        direction = Direction.Down;
                        break;
                }
                if (DirectionCheck(direction, Player.Position))
                {
                    Player.Move(direction);
                    this.FightMode = MonsterOverlapCheck(Player.Position);
                }
                if (Player.Position[0] == Door.Position[0] && Player.Position[1] == Door.Position[1])
                {
                    NextFloor();
                }
            }
            else
            {
                if(key != ConsoleKey.Enter)
                {
                    return;
                }
                Fight();
            }
        }

        public void Fight()
        {
            Monster monster = Monsters.Find(x => x.id == FightMode);
            monster.Defend(Player.Attack(random.Next(100)));
            if(monster.Hp <= 0)
            {
                return;
            }
            Player.Defend(monster.Attack(random.Next(100)));
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
            floor++;
            Setup();
        }

        public void Setup()
        {
            MonsterSetup(3 + floor);
            int xPos, yPos;
            do
            {
                xPos = random.Next(40);
                yPos = random.Next(20);
            } while ((xPos == Player.Position[0] && yPos == Player.Position[1]) && MonsterOverlapCheck([xPos, yPos]) != -1);
            Door.Position[0] = xPos;
            Door.Position[1] = yPos;
        }
        public void MonsterSetup(int amount)
        {
            Monsters = [];
            for(int i = 0; i < amount; i++)
            {
                int xPos , yPos;
                do {
                    xPos = random.Next(40);
                    yPos = random.Next(20);
                } while ((xPos == Player.Position[0] && yPos == Player.Position[0]) && MonsterOverlapCheck([xPos,yPos]) != -1);
                Monsters.Add(Monster.CreateMonster(i + 1 , random.Next(3), [xPos, yPos]));
            }
        }

        private int MonsterOverlapCheck(int[] position)
        {
            foreach (Monster monster in Monsters)
            {
                if (position[0] == monster.Position[0] && position[1] == monster.Position[1])
                {
                    return monster.id;
                }
            }
            return -1;
        }
    }
}
