using DungeonGame.Core;

namespace DungeonGame.UI
{
    public class BoardView
    {
        private readonly string[] baseMap;
        private char[][] roundMap = new char[22][];
        private int floor = 0;

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
            baseMap[11] = baseMap[11] + "    *           *";
            baseMap[12] = baseMap[12] + "    * - Road    *";
            baseMap[13] = baseMap[13] + "    * ■ Mud     *";
            baseMap[14] = baseMap[14] + "    *************";
            for (int i = 0; i < roundMap.Length; i++)
            {
                roundMap[i] = baseMap[i].ToCharArray();
            }
        }

        public void setRoundMap(Game game)
        {
            for (int i = 0; i < roundMap.Length; i++)
            {
                roundMap[i] = baseMap[i].ToCharArray();
            }
            for (int i = 0; i < game.Map.Board.GetLength(0); i++)
            {
                for (int j = 0; j < game.Map.Board.GetLength(1); j++)
                {
                    switch (game.Map.Board[i, j])
                    {
                        case FloorType.Mud:
                            roundMap[i + 1][j + 1] = '■';
                            break;
                        case FloorType.Road:
                            roundMap[i + 1][j + 1] = '_';
                            break;
                        case FloorType.Tree:
                            roundMap[i + 1][j + 1] = '*';
                            break;
                    }

                }
            }
        }

        public void Refresh(Game game)
        {
            if(floor != game.Stats.Floor)
            {
                floor = game.Stats.Floor;
                setRoundMap(game);
            }
            char[][] tempMap = new char[roundMap.Length][];
            for (int i = 0; i < roundMap.Length; i++)
            {
                tempMap[i] = new char[roundMap[i].Length];               
                for(int j = 0; j < roundMap[i].Length; j++)
                {
                    tempMap[i][j] = roundMap[i][j];
                }
            }
            string topInfo = "*****Floor " + game.Stats.Floor + "*****Steps: " + game.Stats.Steps + "*****Kills " + game.Stats.Kills;
            Console.WriteLine(topInfo);

            tempMap[game.Map.Door.Position.X + 1][game.Map.Door.Position.Y + 1] = '¶';
            tempMap[game.Map.Player.Position.X + 1][game.Map.Player.Position.Y + 1] = '+';

            foreach (var item in game.Map.Items)
            {
                tempMap[item.Position.X + 1][item.Position.Y + 1] = getItemSymbol(item.Type);

            }

            foreach (var monster in game.Map.Monsters)
            {
                tempMap[monster.Position.X + 1][monster.Position.Y + 1] = getMonsterSymbol(monster.Type);
            }

            for (int i = 0; i < tempMap.Length; i++)
            {
                Console.WriteLine(tempMap[i]);
            }
        }

        private char getMonsterSymbol(MonsterType type)
        {
            return type switch
            {
                MonsterType.Giganto => '§',
                MonsterType.Normalo => '$',
                MonsterType.Attacko => '#',
                _ => throw new Exception("Invalid Monster Type"),
            };
        }

        private char getItemSymbol(ItemType type)
        {
            return type switch
            {
                ItemType.Crit => 'C',
                ItemType.Damage => 'D',
                ItemType.Heal => 'H',
                _ => throw new Exception("Invalid Item Type"),
            };
        }
    }
}
