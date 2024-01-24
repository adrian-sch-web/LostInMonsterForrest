namespace DungeonGame.Core
{
    public class Placeable(int[] _position)
    {
        public int[] Position { get; set; } = _position;

        public bool OnSameSpot(int[] spot)
        {
            return Position[0] == spot[0] && Position[1] == spot[1];
        }
        
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Position[0]--;
                    break;
                case Direction.Right:
                    Position[0]++;
                    break;
                case Direction.Up:
                    Position[1]--;
                    break;
                case Direction.Down:
                    Position[1]++;
                    break;
            }
        }
    }
}
