namespace DungeonGame.Core
{
    public class Placeable(Position _position)
    {
        public Position Position { get; set; } = _position;

        public bool OnSameSpot(Position spot)
        {
            return Position.X == spot.X && Position.Y == spot.Y;
        }
        
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Position.X--;
                    break;
                case Direction.Right:
                    Position.X++;
                    break;
                case Direction.Up:
                    Position.Y--;
                    break;
                case Direction.Down:
                    Position.Y++;
                    break;
            }
        }
    }
}
