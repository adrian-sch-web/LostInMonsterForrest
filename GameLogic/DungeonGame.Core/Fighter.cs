﻿namespace DungeonGame.Core
{
    public class Fighter(Position _Position, int MaxHp, int _Hp, int _Damage, int _CritChance, double _StaminaPerRound) : Placeable(_Position)
    {
        public int MaxHp { get; set; } = MaxHp;
        public int Hp { get; set; } = _Hp;
        public int Damage { get; set; } = _Damage;
        public int CritChance { get; set; } = _CritChance;
        public double Stamina { get; set; } = 0;
        public double StaminaPerRound { get; set; } = _StaminaPerRound;

        public Attack Attack(int critRoll, Attack attack)
        {
            if (CritChance > critRoll)
            {
                attack.CriticalStrike = true;
                attack.Damage = Damage * 2;
            }
            else
            {
                attack.CriticalStrike = false;
                attack.Damage = Damage;
            }
            return attack;
        }

        public Attack RiskyAttack(int critRoll, int hitRoll, Attack attack)
        {
            if (hitRoll < 10)
            {
                attack.Damage = 0;
                return attack;
            }
            return Attack(critRoll / 2, attack);
        }

        public void Defend(int damage)
        {
            Hp -= damage;
            if (Hp < 0)
            {
                Hp = 0;
            }
        }

        public void WeakDefend(int damage)
        {
            Hp -= (int)(1.5 * damage);
            if (Hp < 0)
            {
                Hp = 0;
            }
        }
    }
}
