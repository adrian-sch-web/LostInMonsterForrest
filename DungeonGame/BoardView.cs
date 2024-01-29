using DungeonGame.Core;

namespace DungeonGame.UI
{
    public class BoardView
    {
        private readonly string[] baseMap;

        public BoardView(Position size)
        {
            baseMap = new string[size.Y + 2];
            baseMap[0] = new string('*', size.X + 2);
            for (int i = 0; i < size.Y; i++)
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
            string[] tempMap = new string[baseMap.Length];
            for (int i = 0; i < tempMap.Length; i++)
            {
                tempMap[i] = baseMap[i];
            }
            string topInfo = "*****Floor " + game.Stats.Floor + "*****Steps: " + game.Stats.Steps + "*****Kills " + game.Stats.Kills;
            tempMap[0] = String.Concat(topInfo, tempMap[0].AsSpan(topInfo.Length));

            tempMap[game.Map.Door.Position.Y + 1] = PrintSymbol(game.Map.Door.Position, tempMap[game.Map.Door.Position.Y + 1], "¶");
            tempMap[game.Map.Player.Position.Y + 1] = PrintSymbol(game.Map.Player.Position, tempMap[game.Map.Player.Position.Y + 1], "+");

            foreach (var item in game.Map.Items)
            {
                tempMap[item.Position.Y + 1] = PrintSymbol(item.Position, tempMap[item.Position.Y + 1], getItemSymbol(item.Type));

            }

            foreach (var monster in game.Map.Monsters)
            {
                tempMap[monster.Position.Y + 1] = PrintSymbol(monster.Position, tempMap[monster.Position.Y + 1], getMonsterSymbol(monster.Type));
            }

            for (int i = 0; i < tempMap.Length; i++)
            {
                Console.WriteLine(tempMap[i]);
            }
        }

        private string getMonsterSymbol(MonsterType type)
        {
            return type switch
            {
                MonsterType.Giganto => "§",
                MonsterType.Normalo => "$",
                MonsterType.Attacko => "#",
                _ => throw new Exception("Invalid Monster Type"),
            };
        }

        private string getItemSymbol(ItemType type)
        {
            return type switch
            {
                ItemType.Crit => "C",
                ItemType.Damage => "D",
                ItemType.Heal => "H",
                _ => throw new Exception("Invalid Item Type"),
            };
        }

        private string PrintSymbol(Position position, string line, string symbol)
        {
            line = string.Concat(line.AsSpan(0, position.X + 1), symbol, line.AsSpan(position.X + 2));
            return line;
        }
    }
}
