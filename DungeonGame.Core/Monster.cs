namespace DungeonGame.Core
{
    public class Monster(int _id, MonsterType _Type, Position _Position, int _MaxHp, int _Hp, int _Damage, int _CritChance, double _StaminaPerRound, Position _Destination) : Fighter(_Position, _MaxHp, _Hp, _Damage, _CritChance, _StaminaPerRound)
    {
        public MonsterType Type { get; } = _Type;
        public int Id { get; } = _id;
        public Position Destination { get; set; } = _Destination;


        public static Monster CreateMonster(int id, MonsterType type, Position position, Position destination)
        {
            return type switch
            {
                MonsterType.Giganto => new Monster(id, type, position, 100, 100, 2, 20, 1, destination),
                MonsterType.Normalo => new Monster(id, type, position, 50, 50, 4, 10, 1.1, destination),
                MonsterType.Attacko => new Monster(id, type, position, 30, 30, 10, 40, 1.2, destination),
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