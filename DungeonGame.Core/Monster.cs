namespace DungeonGame.Core
{
    public class Monster(int _id,  MonsterType _Type,int[] _Position, int _Hp, int _Damage, int _CritChance) : Fighter(_Position,_Hp,_Damage,_CritChance)
    {
        public MonsterType Type { get; } = _Type;
        public int Id { get; } = _id;

        public static Monster CreateMonster(int id, MonsterType type,int[] position)
        {
            switch(type)
            {
                case MonsterType.Giganto:
                    return new Monster(id, type, position, 100, 2, 20);
                case MonsterType.Normalo:
                    return new Monster(id, type, position, 50, 4, 10);
                case MonsterType.Attacko:
                    return new Monster(id, type, position, 30, 10, 40);
                default:
                    return null;
            }
        }
    }

    public enum MonsterType
    {
        Giganto,
        Normalo,
        Attacko
    }
}