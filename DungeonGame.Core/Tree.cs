namespace DungeonGame.Core
{
    public class Tree(Position position) : Placeable(position)
    {


        public static List<Position>[] GetTreeFormations()
        {
            List<Position>[] treeFormations = {
                [
                    new(9,5), new(10,5), new(9,6), new(10,6), new(8,7), new(9,7), new(10,7), new(11,7), new(8,8), new(9,8), 
                    new(10,8), new(11,8), new(7,9), new(8,9), new(9,9), new(10,9), new(11,9), new(12,9), new(7,10), new(8,10),
                    new(9,10), new(10,10), new(11,10), new(12,10), new(8,11), new(9,11), new(10,11), new(11,11), new(8,12),
                    new(9,12), new(10,12), new(11,12), new(9,13), new(10,13), new(9,14), new(10,14)
                    ],
                [
                    new(5,4), new(6,4), new(5,5), new(6,5), new(5,6), new(6,6), new(5,7), new(6,7), new(5,8), new(6,8), new(0,9),
                    new(1,9), new(2,9), new(3,9), new(4,9), new(5,9), new(6,9), new(13,9), new(14,9), new(15,9), new(16,9),
                    new(17,9), new(18,9), new(19,9), new(0,10), new(1,10), new(2,10), new(3,10), new(4,10), new(5,10), new(6,10),
                    new(13,10), new(14,10), new(15,10), new(16,10), new(17,10), new(18,10), new(19,10), new(13,11), new(14,11),
                    new(13,12), new(14,12), new(13,13), new(14,13), new(13,14), new(14,14), new(13,15), new(14,15)
                    ],
                [
                    new(0,0), new(1,0), new(2,0), new(3,0), new(4,0), new(5,0), new(14,0), new(15,0), new(16,0), new(17,0),
                    new(18,0), new(19,0), new(0,1), new(1,1), new(2,1), new(3,1), new(4,1), new(15,1), new(16,1), new(17,1),
                    new(18,1), new(19,1), new(0,2), new(1,2), new(2,2), new(3,2), new(16,2), new(17,2), new(18,2), new(19,2),
                    new(0,3), new(1,3), new(2,3), new(17,3), new(18,3), new(19,3), new(0,4), new(1,4), new(18,4), new(0,5),
                    new(19,5), new(0,14), new(19,14), new(0,15), new(1,15), new(18,15), new(19,15), new(0,16), new(1,16), new(2,16),
                    new(17,16), new(18,16), new(19,16), new(0,17), new(1,17), new(2,17), new(16,17), new(17,17), new(18,17),
                    new(19,17), new(0,18), new(1,18), new(2,18), new(3,18), new(4,18), new(15,18), new(16,18), new(17,18),
                    new(18,18), new(19,18), new(0,19), new(1,19), new(2,19), new(3,19), new(4,19), new(5,19), new(14,19),
                    new(15,19), new(16,19), new(17,19), new(18,19), new(19,19)
                    ],
                [
                    new(9,6), new(10,6), new(9,7), new(10,7), new(9,8), new(10,8), new(6,9), new(7,9), new(8,9), new(9,9), new(10,9),
                    new(11,9), new(12,9), new(13,9), new(6,10), new(7,10), new(8,10), new(9,10), new(10,10), new(11,10), new(12,10),
                    new(13,10), new(9,11), new(10,11), new(9,12), new(10,12), new(9,13), new(10,13)
                    ],
                [
                    new(9,0), new(10,0), new(9,1), new(10,1), new(9,2), new(10,2), new(9,3), new(10,3), new(9,4), new(10,4), new(9,5),
                    new(10,5), new(0,9), new(1,9), new(2,9), new(3,9), new(4,9), new(5,9), new(14,9), new(15,9), new(16,9), new(17,9),
                    new(18,9), new(19,9), new(0,10), new(1,10), new(2,10), new(3,10), new(4,10), new(5,10), new(14,10), new(15,10),
                    new(16,10), new(17,10), new(18,10), new(19,10), new(9,14), new(10,14), new(9,15), new(10,15), 
                    new(9,16), new(10,16), new(9,17), new(10,17), new(9,18), new(10,18), new(9,19), new(10,19),
                    ],
                [
                    new(0,3), new(1,3), new(2,3), new(3,3), new(4,3), new(5,3), new(6,3), new(7,3), new(8,3), new(9,3), new(10,3),
                    new(11,3), new(12,3), new(13,3), new(0,4), new(1,4), new(2,4), new(3,4), new(4,4), new(5,4), new(6,4), new(7,4),
                    new(8,4), new(9,4), new(10,4), new(11,4), new(12,4), new(13,4), new(6,15), new(7,15), new(8,15), new(9,15),
                    new(10,15), new(11,15), new(12,15), new(13,15), new(14,15), new(15,15), new(16,15), new(17,15), new(18,15),
                    new(19,15), new(6,16), new(7,16), new(8,16), new(9,16), new(10,16), new(11,16), new(12,16), new(13,16), 
                    new(14,16), new(15,16), new(16,16), new(17,16), new(18,16), new(19,16)
                    ],
                [
                    new(3,0), new(4,0), new(5,1), new(6,1), new(7,2), new(8,2), new(9,3), new(10,3), new(11,4), new(12,4), new(13,5), 
                    new(14,5), new(15,6), new(16,6), new(5,14), new(6,14), new(7,15), new(8,15), new(9,16), new(10,16), new(11,17), 
                    new(12,17), new(13,18), new(14,18), new(15,19), new(16,19)
                        ]
            };
            return treeFormations;
        }
    }
}
