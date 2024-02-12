namespace DungeonGame.Core
{
    public class Game
    {
        public ProgressStats Stats = new();
        public Map Map = new();
        public bool recordSubmitted = false;
        public bool IsRunning { get; set; } = true;
        public int FightMode { get; set; } = -1;        //Index of monster in fight -1 if not in fight
        public Attack[] Attacks = [];
        private readonly Random random = new();

        public Game()
        {
            Map.Setup(0);
        }
        public void Action(Input input)
        {
            if (FightMode == -1)
            {
                Direction direction = InputToDirection(input);
                MoveTurn(direction);
            }
            else
            {
                FightTurn(input == Input.RiskyAttack);
            }
        }

        public void MoveTurn(Direction direction)
        {

            Stats.Steps++;
            Map.Player.Move(direction);

            if (CollisionChecks())
            {
                return;
            }

            MonsterTurn();
        }

        public void FightTurn(bool risky)
        {
            Attacks = [];
            if (Map.Player.Hp <= 0)
            {
                IsRunning = false;
                return;
            }
            Fight(risky);
        }

        public bool CollisionChecks()
        {
            FightMode = Map.OnMonster(Map.Player.Position);
            if (FightMode != -1)
            {
                return true;
            }

            Item? item = Map.Items.Find(x => x.Id == Map.OnItem(Map.Player.Position));
            if (item is not null)
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
                if (monster.Distance(Map.Player.Position) <= 5)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        direction = Map.FindPath(monster.Position, Map.Player.Position);
                        //List<Direction> directions = monster.OptimalMove(Map.Player.Position);
                        //if (directions.Count > 0)
                        //{
                        //    direction = directions[random.Next(directions.Count)];
                        //}
                        Move(monster, direction);
                    }
                }
                else
                {
                    direction = Map.FindPath(monster.Position, monster.Destination);
                    //List<Direction> directions = monster.OptimalMove(monster.Destination);
                    //if (directions.Count > 0)
                    //{
                    //    direction = directions[random.Next(directions.Count)];
                    //}
                    Move(monster, direction);
                    if (monster.OnSameSpot(monster.Destination))
                    {
                        monster.Destination = Map.GetRandomPosition();
                    }
                }
            }
            FightMode = Map.OnMonster(Map.Player.Position);
        }

        public void Move(Monster monster, Direction direction)
        {
            Position newPosition = monster.Position.GetNeighbourPosition(direction);

            if (Map.OnTree(newPosition) || Map.OnMonster(newPosition) != -1 || Map.Door.OnSameSpot(newPosition) || Map.OnItem(newPosition) != -1)
            {
                direction = Direction.Idle;
                monster.Destination = Map.GetRandomPosition();
            }
            monster.Move(direction);
        }

        public bool Fight(bool risky)
        {
            Monster? monster = Map.Monsters.Find(x => x.Id == FightMode);
            if (monster is null)
            {
                return false;
            }
            if (monster.Hp == 0)
            {
                FightMode = -1;
                Attacks = [];
                Map.Monsters.Remove(monster);
                Stats.Kills++;
                return false;
            }
            Attack playerAttack = new(monster, true);
            if (risky)
            {
                playerAttack.Risky = true;
                playerAttack = Map.Player.RiskyAttack(random.Next(100), random.Next(100), playerAttack);
            }
            else
            {
                playerAttack = Map.Player.Attack(random.Next(100), playerAttack);
            }
            monster.Defend(playerAttack.Damage);
            if (monster.Hp <= 0)
            {
                playerAttack.Kill = true;
                Attacks = [playerAttack];
                return false;
            }
            Attack monsterAttack = new(monster, false);
            monsterAttack = monster.Attack(random.Next(100), monsterAttack);
            if (playerAttack.Risky)
            {
                monsterAttack.Damage = (int)(monsterAttack.Damage * 1.5);
                Map.Player.WeakDefend(monsterAttack.Damage);
            }
            else
            {
                Map.Player.Defend(monsterAttack.Damage);
            }
            Attacks = [playerAttack, monsterAttack];
            return Map.Player.Hp > 0;
        }

        private Direction InputToDirection(Input input)
        {
            return input switch
            {
                Input.Left => Direction.Left,
                Input.Right => Direction.Right,
                Input.Up => Direction.Up,
                Input.Down => Direction.Down,
                _ => Direction.Idle,
            };
        }
        public void NextFloor()
        {
            Stats.Floor++;
            Map.Setup(Stats.Floor);
        }
    }
}
