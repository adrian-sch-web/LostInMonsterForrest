using System.Collections;

namespace DungeonGame.Core
{
    public class Floor
    {
        private static Position Size { get; } = new(20, 20);
       
        public static List<FloorType[,]> GetBoards()
        {
            List<FloorType[,]> formations = [];
            formations.Add(new FloorType[Size.X, Size.Y]);

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 0; i < 5; i++)
            {
                formations[1][5, i + 4] = FloorType.Tree;
                formations[1][6, i + 4] = FloorType.Tree;
                formations[1][13, i + 11] = FloorType.Tree;
                formations[1][14, i + 11] = FloorType.Tree;
            }
            for (int i = 0; i < 7; i++)
            {
                formations[1][i, 9] = FloorType.Tree;
                formations[1][i, 10] = FloorType.Tree;
                formations[1][i + 13, 9] = FloorType.Tree;
                formations[1][i + 13, 10] = FloorType.Tree;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 4; j < 9; j++)
                {
                    formations[1][i, j] = FloorType.Mud;
                    formations[1][i + 15, j + 7] = FloorType.Mud;
                }
            }
            for (int i = 0; i < 20; i++)
            {
                formations[1][9, i] = FloorType.Road;
                formations[1][10, i] = FloorType.Road;
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i + j > 5)
                    {
                        continue;
                    }
                    formations[2][i, j] = FloorType.Tree;
                    formations[2][19 - i, j] = FloorType.Tree;
                    formations[2][i, 19 - j] = FloorType.Tree;
                    formations[2][19 - i, 19 - j] = FloorType.Tree;
                }
            }
            for (int i = 8; i < 12; i++)
            {
                for (int j = 8; j < 12; j++)
                {
                    formations[2][i, j] = FloorType.Mud;
                }
            }
            for (int i = 9; i < 11; i++)
            {
                formations[2][i, 8] = FloorType.Mud;
                formations[2][i, 11] = FloorType.Mud;
                formations[2][8, i] = FloorType.Mud;
                formations[2][11, i] = FloorType.Mud;
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 5; i < 15; i++)
            {
                formations[3][9, i] = FloorType.Tree;
                formations[3][10, i] = FloorType.Tree;
            }
            for (int i = 0; i < 4; i++)
            {
                formations[3][i + 5, 9] = FloorType.Tree;
                formations[3][i + 5, 10] = FloorType.Tree;
                formations[3][i + 11, 9] = FloorType.Tree;
                formations[3][i + 11, 10] = FloorType.Tree;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    formations[3][i, j] = FloorType.Mud;
                    formations[3][i + 17, j] = FloorType.Mud;
                    formations[3][i, j + 17] = FloorType.Mud;
                    formations[3][i + 17, j + 17] = FloorType.Mud;
                }
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 0; i < 6; i++)
            {
                formations[4][9, i] = FloorType.Tree;
                formations[4][10, i] = FloorType.Tree;
                formations[4][9, i + 14] = FloorType.Tree;
                formations[4][10, i + 14] = FloorType.Tree;
                formations[4][i, 9] = FloorType.Tree;
                formations[4][i, 10] = FloorType.Tree;
                formations[4][i + 14, 9] = FloorType.Tree;
                formations[4][i + 14, 10] = FloorType.Tree;
            }
            for(int i = 6; i < 14; i++)
            {
                for( int j = 6; j < 14; j++)
                {
                    formations[4][i, j] = FloorType.Road;
                }
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 0; i < 14; i++)
            {
                formations[5][i, 3] = FloorType.Tree;
                formations[5][i, 4] = FloorType.Tree;
                formations[5][i + 6, 15] = FloorType.Tree;
                formations[5][i + 6, 16] = FloorType.Tree;
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 0; i < 6; i++)
            {
                formations[6][3 + 2 * i, i] = FloorType.Tree;
                formations[6][4 + 2 * i, i] = FloorType.Tree;
                formations[6][5 + 2 * i, i + 14] = FloorType.Tree;
                formations[6][6 + 2 * i, i + 14] = FloorType.Tree;
            }

            formations.Add(new FloorType[Size.X, Size.Y]);
            for (int i = 5; i < 15; i++)
            {
                formations[7][9, i] = FloorType.Tree;
                formations[7][10, i] = FloorType.Tree;
            }
            for (int i = 7; i < 13; i++)
            {
                formations[7][8, i] = FloorType.Tree;
                formations[7][11, i] = FloorType.Tree;
            }
            formations[7][7, 9] = FloorType.Tree;
            formations[7][12, 9] = FloorType.Tree;
            formations[7][7, 10] = FloorType.Tree;
            formations[7][12, 10] = FloorType.Tree;
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    formations[7][i, j] = FloorType.Road;
                }
            }
            for (int i = 14; i < 18; i++)
            {
                for (int j = 8; j < 12; j++)
                {
                    formations[7][i, j] = FloorType.Mud;
                }
            }
            return formations;
        }
    }
}
