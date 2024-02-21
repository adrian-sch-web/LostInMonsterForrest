﻿namespace DungeonGame.Core
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
            Map.Setup(1);
        }

        public void MoveTurn(Direction direction)
        {
            Position playerPosition = Map.Player.Position;
            if (CollisionChecks())
            {
                return;
            }
            if (!playerPosition.GetNeighbourPosition(direction).InField(Map.Size) || Map.OnTree(playerPosition.GetNeighbourPosition(direction)))
            {
                return;
            }
            double moveCost = Map.MoveCost(playerPosition);
            if (Map.Player.Stamina >= moveCost)
            {
                Stats.Steps++;
                Map.Player.Move(direction);
                Map.Player.Stamina -= moveCost;
                if (CollisionChecks())
                {
                    Map.Player.Stamina += Map.Player.StaminaPerRound;
                    return;
                }
                moveCost = Map.MoveCost(Map.Player.Position);
                if (Map.Player.Stamina >= moveCost)
                {
                    return;
                }
                MonsterTurn();
                Map.Player.Stamina += Map.Player.StaminaPerRound;
            }
            else
            {
                MonsterTurn();
                Map.Player.Stamina += Map.Player.StaminaPerRound;
            }
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
                double moveCost = Map.MoveCost(monster.Position);

                monster.Stamina += monster.StaminaPerRound;
                Direction direction;
                if (monster.Distance(Map.Player.Position) <= 5)
                {
                    monster.Stamina += monster.StaminaPerRound;
                    monster.Destination = Map.Player.Position;
                }
                while (monster.Stamina >= moveCost)
                {
                    direction = Map.FindPath(monster.Position, monster.Destination);
                    monster.Move(direction);
                    monster.Stamina -= moveCost;
                }
                if (monster.OnSameSpot(monster.Destination) && !monster.OnSameSpot(Map.Player.Position))
                {
                    monster.Destination = Map.GetRandomPosition();
                }

            }
            FightMode = Map.OnMonster(Map.Player.Position);
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

        public void NextFloor()
        {
            Stats.Floor++;
            Map.Setup(Stats.Floor);
        }
    }
}
