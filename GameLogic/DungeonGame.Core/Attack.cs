namespace DungeonGame.Core
{
    public class Attack(Monster _monster, bool _attacker)
    {
        public bool Attacker { get; set; } = _attacker;
        public Monster Monster { get; set; } = _monster;
        public bool Risky {  get; set; } = false;
        public bool CriticalStrike { get; set; } = false;
        public bool Kill { get; set; } = false;
        public int Damage {  get; set; }
    }
}
