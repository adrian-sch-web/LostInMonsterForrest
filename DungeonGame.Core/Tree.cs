namespace DungeonGame.Core
{
    public class Tree(Position position) : Placeable(position)
    {


        public static List<Position>[] GetTreeFormations()
        {
            List<Position>[] treeFormations = [[], [], [], [], [], [], []];
            for (int i = 0; i < 10; i++)
            {
                treeFormations[0].Add(new(9, i + 5));
                treeFormations[0].Add(new(10, i + 5));
            }
            for (int i = 0; i < 6; i++)
            {
                treeFormations[0].Add(new(8, i + 7));
                treeFormations[0].Add(new(11, i + 7));
            }
            treeFormations[0].Add(new(7, 9));
            treeFormations[0].Add(new(12, 9));
            treeFormations[0].Add(new(7, 10));
            treeFormations[0].Add(new(12, 10));

            for (int i = 0; i < 5; i++)
            {
                treeFormations[1].Add(new(5, i + 4));
                treeFormations[1].Add(new(6, i + 4));
                treeFormations[1].Add(new(13, i + 11));
                treeFormations[1].Add(new(14, i + 11));
            }
            for (int i = 0; i < 7; i++)
            {
                treeFormations[1].Add(new(i, 9));
                treeFormations[1].Add(new(i, 10));
                treeFormations[1].Add(new(i + 13, 9));
                treeFormations[1].Add(new(i + 13, 10));
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i + j > 5)
                    {
                        continue;
                    }
                    treeFormations[2].Add(new(i, j));
                    treeFormations[2].Add(new(19 - i, j));
                    treeFormations[2].Add(new(i, 19 - j));
                    treeFormations[2].Add(new(19 - i, 19 - j));
                }
            }

            for (int i = 0; i < 10; i++)
            {
                treeFormations[3].Add(new(9, i + 5));
                treeFormations[3].Add(new(10, i + 5));
            }
            for (int i = 0; i < 4; i++)
            {
                treeFormations[3].Add(new(i + 5, 9));
                treeFormations[3].Add(new(i + 5, 10));
                treeFormations[3].Add(new(i + 11, 9));
                treeFormations[3].Add(new(i + 11, 10));
            }

            for (int i = 0; i < 6; i++)
            {
                treeFormations[4].Add(new(9, i));
                treeFormations[4].Add(new(10, i));
                treeFormations[4].Add(new(9, i + 14));
                treeFormations[4].Add(new(10, i + 14));
                treeFormations[4].Add(new(i, 9));
                treeFormations[4].Add(new(i, 10));
                treeFormations[4].Add(new(i + 14, 9));
                treeFormations[4].Add(new(i + 14, 10));
            }

            for (int i = 0; i < 14; i++)
            {
                treeFormations[5].Add(new(i, 3));
                treeFormations[5].Add(new(i, 4));
                treeFormations[5].Add(new(i + 6, 15));
                treeFormations[5].Add(new(i + 6, 16));
            }

            for (int i = 0; i < 6; i++)
            {
                treeFormations[6].Add(new(3 + 2 * i, i));
                treeFormations[6].Add(new(4 + 2 * i, i));
                treeFormations[6].Add(new(5 + 2 * i, i + 14));
                treeFormations[6].Add(new(6 + 2 * i, i + 14));
            }

            return treeFormations;
        }
    }
}
