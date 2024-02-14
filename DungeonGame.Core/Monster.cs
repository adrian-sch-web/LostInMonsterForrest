namespace DungeonGame.Core
{
    public class Monster(int _id, MonsterType _Type, Position _Position, int _MaxHp, int _Hp, int _Damage, int _CritChance, double stamPerRound, Position destination) : Fighter(_Position, _MaxHp, _Hp, _Damage, _CritChance)
    {
        public MonsterType Type { get; } = _Type;
        public int Id { get; } = _id;
        public Position Destination { get; set; } = destination;
        public double Stamina { get; set; } = 0;
        public double StaminaPerRound { get; set; } = stamPerRound;


        public static Monster CreateMonster(int id, MonsterType type, Position position, Position destination)
        {
            return type switch
            {
                MonsterType.Giganto => new Monster(id, type, position, 100, 100, 2, 20, 0.8, destination),
                MonsterType.Normalo => new Monster(id, type, position, 50, 50, 4, 10, 1, destination),
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