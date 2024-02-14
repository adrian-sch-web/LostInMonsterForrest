namespace DungeonGame.Core
{
    public class Player : Fighter
    {
        public Player(Position _Position, int _MaxHp, int _Hp, int _Damage, int _CritChance, double _StaminaPerRound) : base(_Position, _MaxHp, _Hp, _Damage, _CritChance, _StaminaPerRound)
        {
            Stamina = 1;
        }
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
                    if (Hp > MaxHp)
                    {
                        Hp = MaxHp;
                    }
                    break;
            }
        }
    }
}
