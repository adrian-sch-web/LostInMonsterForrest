namespace DungeonGame.Core
{
    public class Player(Position _Position, int _MaxHp, int _Hp, int _Damage, int _CritChance) : Fighter(_Position, _MaxHp, _Hp, _Damage, _CritChance)
    {
        public void Collect(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Crit:
                    CritChance += 5;
                    break;
                case ItemType.Damage:
                    Damage += 5;
                    break;
                case ItemType.Heal:
                    Hp += 5;
                    if(Hp > MaxHp)
                    {
                        Hp = MaxHp;
                    }
                    break;
            }
        }
    }
}
