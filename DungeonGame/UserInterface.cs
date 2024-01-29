using DungeonGame.Core;

namespace DungeonGame.UI
{
    internal class UserInterface
    {
        private readonly string[] baseMap;
        private bool RiskyAttack;

        public UserInterface(Position size)
        {
            baseMap = new string[size.Y + 2];
            baseMap[0] = new string('*', size.X + 2);
            for(int i = 0; i < size.Y; i++)
            {
                baseMap[i + 1] = "*" + new string(' ', size.X) + "*";
            }
            baseMap[size.Y + 1] = baseMap[0];
            baseMap[2] = baseMap[2] + "    Legend:";
            baseMap[3] = baseMap[3] + "    *************";
            baseMap[4] = baseMap[4] + "    * + You     *";
            baseMap[5] = baseMap[5] + "    * ¶ Door    *";
            baseMap[6] = baseMap[6] + "    *           *";
            baseMap[7] = baseMap[7] + "    * Monsters: *";
            baseMap[8] = baseMap[8] + "    * § Giganto *";
            baseMap[9] = baseMap[9] + "    * $ Normalo *";
            baseMap[10] = baseMap[10] + "    * # Attacko *";
            baseMap[11] = baseMap[11] + "    *************";
        }

        public void Refresh(Game game)
        {
            Console.Clear();
            if (!game.IsRunning)
            {
                Death(game);
                return;
            }
            Console.WriteLine(String.Concat("HP: " + game.Map.Player.Hp + "    AD: " + game.Map.Player.Damage + "    Crit Chance: " + game.Map.Player.CritChance + "%"));
            if (game.FightMode != -1)
            {
                RefreshFight(game);
            }
            else
            {
                RefreshBoard(game);
            }
        }

        private void RefreshFight(Game game)
        {
            Monster? monster = game.Map.Monsters.Find(x => x.Id == game.FightMode);
            if(monster is null)
            {
                return;
            }
            Console.WriteLine("\n FIGHT!");
            Console.WriteLine("\nYou" + "  vs  " + monster.Type);
            Console.WriteLine("\n" + game.Map.Player.Hp + "      " + monster.Hp);
            Console.WriteLine("\n\n");
            if (RiskyAttack)
            {
                Console.WriteLine("Normal Attack    " + "\x1B[4m" + "Risky Attack" + "\x1B[0m");
            }
            else
            {
                Console.WriteLine("\x1B[4m" + "Normal Attack" + "\x1B[0m" + "    Risky Attack" );
            }
            foreach(var attack in game.Attacks) 
            {
                PrintAttackMessage(attack);
            }
            
        }
        private void RefreshBoard(Game game)
        {
            string[] tempMap = new string[baseMap.Length];
            for (int i = 0; i < tempMap.Length; i++)
            {
                tempMap[i] = baseMap[i];
            }
            string topInfo = "*****Floor " + game.Stats.Floor + "*****Steps: " + game.Stats.Steps +  "*****Kills " + game.Stats.Kills;
            tempMap[0] = String.Concat(topInfo, tempMap[0].AsSpan(topInfo.Length));

            tempMap[game.Map.Door.Position.Y + 1] = PrintSymbol(game.Map.Door.Position, tempMap[game.Map.Door.Position.Y + 1], "¶");
            tempMap[game.Map.Player.Position.Y + 1] = PrintSymbol(game.Map.Player.Position, tempMap[game.Map.Player.Position.Y + 1], "+");

            foreach(var item in game.Map.Items)
            {
                tempMap[item.Position.Y + 1] = PrintSymbol(item.Position, tempMap[item.Position.Y + 1], getItemSymbol(item.Type));

            }

            foreach ( var monster in game.Map.Monsters)
            {
                tempMap[monster.Position.Y + 1] = PrintSymbol(monster.Position, tempMap[monster.Position.Y + 1], getMonsterSymbol(monster.Type));
            }

            for(int i = 0; i < tempMap.Length; i++) 
            {
                Console.WriteLine(tempMap[i]);
            }
        }

        private string getMonsterSymbol(MonsterType type)
        {
            switch (type)
            {
                case MonsterType.Giganto:
                    return "§";
                case MonsterType.Normalo:
                    return "$";
                case MonsterType.Attacko:
                    return "#";
                default:
                    throw new Exception("Invalid Monster Type");
            }
        }

        private string getItemSymbol(ItemType type)
        {
            switch (type)
            {
                case ItemType.Crit:
                    return "C";
                case ItemType.Damage:
                    return "D";
                case ItemType.Heal:
                    return "H";
                default:
                    throw new Exception("Invalid Item Type");
            }
        }

        private string PrintSymbol(Position position, string line,  string symbol)
        {
            line = string.Concat(line.AsSpan(0, position.X + 1), symbol, line.AsSpan(position.X + 2));
            return line;
        }

        private void PrintAttackMessage(Attack attack)
        {            
            if (attack.Attacker)
            {
                if(attack.Damage == 0)
                {
                    Console.WriteLine("\n\nOh no, your risky Attack missed the monster");
                    return;
                }
                string risky = "normal";
                if(attack.Risky) 
                { 
                    risky = "risky"; 
                }
                Console.WriteLine("\n\nYou hit " + attack.Monster.Type + " with a " + risky + " Attack.");
                if (attack.CriticalStrike)
                {
                    Console.WriteLine("It was a critical Strike.");
                }
                Console.WriteLine(attack.Monster.Type + " lost " + attack.Damage + " HP");
                if (attack.Kill)
                {
                    Console.WriteLine(attack.Monster.Type + " died");
                }
            }
            else
            {
                Console.WriteLine("\n\n" + attack.Monster.Type + " hit you.");
                if (attack.CriticalStrike)
                {
                    Console.WriteLine("It was a critical Strike.");
                }
                Console.WriteLine("You lost " + attack.Damage + " HP");
            }
            
        }
        public Input WaitUserInput(Game game)
        {
            ConsoleKeyInfo keyInfo;
            if (game.FightMode != -1)
            {
                do
                {
                    keyInfo = Console.ReadKey();
                }
                while (
                    keyInfo.Key != ConsoleKey.LeftArrow &&
                    keyInfo.Key != ConsoleKey.RightArrow &&
                    keyInfo.Key != ConsoleKey.Enter
                );
                if(keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) 
                {
                    RiskyAttack = !RiskyAttack;
                    Refresh(game);
                    return WaitUserInput(game);
                }
                if(RiskyAttack)
                {
                    return Input.RiskyAttack;
                }
                return Input.NormalAttack;
            }
            
            
            do
            { 
                keyInfo = Console.ReadKey();
            }
            while(
                keyInfo.Key != ConsoleKey.LeftArrow &&
                keyInfo.Key != ConsoleKey.RightArrow &&
                keyInfo.Key != ConsoleKey.UpArrow &&
                keyInfo.Key != ConsoleKey.DownArrow
            );
            if (!MoveCheck(game, keyInfo))
            {
                return WaitUserInput(game);
            }
            return KeyToInput(keyInfo.Key);
        }

        private bool MoveCheck(Game game, ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (game.Map.Player.Position.X == 0)
                        return false;
                    break;
                case ConsoleKey.RightArrow:
                    if (game.Map.Player.Position.X == game.Map.Size.X)
                        return false;
                    break;
                case ConsoleKey.UpArrow:
                    if (game.Map.Player.Position.Y == 0)
                        return false;
                    break;
                case ConsoleKey.DownArrow:
                    if (game.Map.Player.Position.Y == game.Map.Size.Y)
                        return false;
                    break;
                default: 
                    return false;
            }
            return true;
        }

        private void Death(Game game)
        {
            Console.Clear();
            Console.WriteLine("\n\nGame Over!\n\n\n");
            Console.WriteLine("You died and made it to Floor " + game.Stats.Floor + "!");
            Console.WriteLine("On your way through the dungeon you made " + game.Stats.Steps + " steps.");
            Console.WriteLine(game.Stats.Kills + " cruel Monsters were defeated by you!");
        }

        public Input KeyToInput(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    return Input.Left;
                case ConsoleKey.RightArrow:
                    return Input.Right;
                case ConsoleKey.UpArrow:
                    return Input.Up;
                case ConsoleKey.DownArrow:
                    return Input.Down;
                default:
                    throw new Exception("Invalid Key");
            }
        }
    }
}
