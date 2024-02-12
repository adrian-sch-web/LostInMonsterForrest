namespace DungeonGame.Core
{
    public class Position
    {
        public int X;
        public int Y;
        public Position(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public Position GetNeighbourPosition(Direction direction)
        {
            return direction switch
            {
                (Direction.Left) => new Position(X - 1, Y),
                (Direction.Right) => new Position(X + 1, Y),
                (Direction.Up) => new Position(X, Y - 1),
                Direction.Down => new Position(X, Y + 1),
                _ => new Position(X, Y),
            };
        }

        public bool SamePosition(Position other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
