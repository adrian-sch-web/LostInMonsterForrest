﻿namespace DungeonGame.Core
{
    public class Monster(int _id,  MonsterType _Type, Position _Position, int _Hp, int _Damage, int _CritChance) : Fighter(_Position,_Hp,_Damage,_CritChance)
    {
        public MonsterType Type { get; } = _Type;
        public int Id { get; } = _id;

        public List<Direction> OptimalMove(Position destination)
        {
            List<Direction> possibleDirections = [];

            if (Position.X > destination.X)
            {
                possibleDirections.Add(Direction.Left);
            }
            if(Position.X < destination.X)
            {
                possibleDirections.Add(Direction.Right);
            }
            if(Position.Y > destination.Y)
            {
                possibleDirections.Add(Direction.Up);
            }
            if (Position.Y < destination.Y)
            {
                possibleDirections.Add(Direction.Down);
            }
            return possibleDirections;
        }

        public static Monster CreateMonster(int id, MonsterType type,Position position)
        {
            return type switch
            {
                MonsterType.Giganto => new Monster(id, type, position, 100, 2, 20),
                MonsterType.Normalo => new Monster(id, type, position, 50, 4, 10),
                MonsterType.Attacko => new Monster(id, type, position, 30, 10, 40),
                _ => throw new Exception("Invalid Monster Type"),
            };
        }
    }

    public enum MonsterType
    {
        Giganto,
        Normalo,
        Attacko
    }
}