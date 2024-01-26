using DungeonGame.Core;

namespace DungeonGame.UI
{
    internal class UserInterface
    {
        public string[] baseMap;

        public UserInterface(int[] size)
        {
            baseMap = new string[size[1] + 2];
            baseMap[0] = new string('*', size[0] + 2);
            for(int i = 0; i < size[1]; i++)
            {
                baseMap[i + 1] = "*" + new string(' ', size[0]) + "*";
            }
            baseMap[size[1] + 1] = baseMap[0];
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
            if(monster == null)
            {
                return;
            }
            Console.WriteLine("\n FIGHT!");
            Console.WriteLine("\nYou" + "  vs  " + monster.Type);
            Console.WriteLine("\n" + game.Map.Player.Hp + "      " + monster.Hp);
            Console.WriteLine("\n\n");
            if (game.RiskyAttack)
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
            string floor = "Floor " + game.Floor;
            tempMap[0] = String.Concat("*" , floor, tempMap[0].AsSpan(floor.Length + 1));

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
                    return " ";
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
                    return " ";
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
        public Input WaitUserInput()
        {
            ConsoleKeyInfo keyInfo;
            do
            { 
                keyInfo = Console.ReadKey();
            }
            while(
                keyInfo.Key != ConsoleKey.LeftArrow &&
                keyInfo.Key != ConsoleKey.RightArrow &&
                keyInfo.Key != ConsoleKey.UpArrow &&
                keyInfo.Key != ConsoleKey.DownArrow &&
                keyInfo.Key != ConsoleKey.Enter
            );
            return KeyToInput(keyInfo.Key);
        }

        private void Death(Game game)
        {
            Console.Clear();
            Console.WriteLine("\n\nGame Over!\n\n\n");
            Console.WriteLine("You died and made it to Floor " + game.Floor + "!");
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
                    return Input.Enter;
            }
        }
    }
}
