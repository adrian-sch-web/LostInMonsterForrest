namespace DungeonGame.Core
{
    public class Game
    {
        public int Floor = 0;
        public int Kills = 0;
        public int Steps = 0;
        public Map Map = new();
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;        //Index of monster in fight -1 if not in fight
        public Attack[] Attacks = [];
        public bool RiskyAttack { get; set; } = false;
        private readonly Random random = new();
        
        public void Press(Input input)
        {
            if (FightMode == -1)
            {
                MoveTurn(input);
            }
            else
            {
                FightTurn(input);
            }
        }

        public void MoveTurn(Input input)
        {
            if (input == Input.Enter)
            {
                return;
            }

            Direction direction = InputToDirection(input);

            if (!Map.DirectionCheck(direction, Map.Player.Position))
            {
                return;
            }
            Steps++;
            Map.Player.Move(direction);
             
            if (CollisionChecks())
            {
                return;
            }

            MonsterTurn();
        }

        public void FightTurn(Input input)
        {
            if (input != Input.Enter)
            {
                RiskyAttack = !RiskyAttack;
                return;
            }
            Attacks = [];
            if (Fight())
            {
                return;
            }
            if (Map.Player.Hp == 0)
            {
                IsRunning = false;
            }
        }

        public bool CollisionChecks()
        {
            FightMode = Map.OnMonster(Map.Player.Position);
            if (FightMode != -1)
            {
                return true;
            }

            Item? item = Map.Items.Find(x => x.Id == Map.OnItem(Map.Player.Position));
            if (item != null)
            {
                Map.Player.Collect(item);
                Map.Items.Remove(item);
                return false;
            }

            if (Map.Door.OnSameSpot(Map.Player.Position))
            {
                NextFloor();
                return true;
            }
            return false;
        }

        public void MonsterTurn()
        {
            foreach (var monster in Map.Monsters)
            {
                Direction direction;
                if (monster.Distance(Map.Player.Position) < 5)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        direction = Direction.Idle;
                        List<Direction> directions = monster.OptimalMove(Map.Player.Position);
                        if (directions.Count > 0)
                        {
                            direction = directions[random.Next(directions.Count)];
                        }
                        Move(monster, direction);
                    }
                }
                else
                {
                    do
                    {
                        direction = (Direction)random.Next(4);
                    } while (!Map.DirectionCheck(direction, monster.Position));
                    Move(monster, direction);
                }
            }
            FightMode = Map.OnMonster(Map.Player.Position);
        }
        
        public void Move(Monster monster, Direction direction)
        {
            Position newPosition = GetNewPosition(monster.Position, direction);

            if (Map.OnMonster(newPosition) != -1 || Map.Door.OnSameSpot(newPosition) || Map.OnItem(newPosition) != -1)
            {
                direction = Direction.Idle;
            }
            monster.Move(direction);
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
                RiskyAttack = false;
                Attacks = [];
                Map.Monsters.Remove(monster);
                Kills++;
                return false;
            }
            Attack playerAttack = new();
            playerAttack.Attacker = true;
            playerAttack.Monster = monster;
            if (RiskyAttack)
            {
                playerAttack.Risky = true;
                playerAttack = Map.Player.RiskyAttack(random.Next(100),playerAttack);
            }
            else
            {
                playerAttack = Map.Player.Attack(random.Next(100),playerAttack);
            }
            monster.Defend(playerAttack.Damage);
            if (monster.Hp <= 0)
            {
                playerAttack.Kill = true;
                Attacks = [playerAttack];
                return false;
            }
            Attack monsterAttack = new();
            monsterAttack.Monster = monster;
            monsterAttack = monster.Attack(random.Next(100),monsterAttack);
            if (playerAttack.Risky)
            {
                monsterAttack.Damage = (int) (monsterAttack.Damage * 1.5);
                Map.Player.WeakDefend(monsterAttack.Damage);
            }
            else
            {
                Map.Player.Defend(monsterAttack.Damage);
            }
            Attacks = [playerAttack, monsterAttack];
            return Map.Player.Hp > 0;
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
                case Direction.Down:
                    return new Position(position.X, position.Y + 1);
                default:
                    return position;
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
                case Input.Down:
                    return Direction.Down;
                default:
                    return Direction.Idle;
            }
        }
        public void NextFloor()
        {
            Floor++;
            Map.Setup(Floor);
        }
    }
}
