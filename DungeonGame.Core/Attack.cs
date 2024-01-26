namespace DungeonGame.Core
{
    public class Attack
    {
        public bool Attacker { get; set; } = false;
        public Monster? Monster { get; set; }
        public bool Risky {  get; set; } = false;
        public bool CriticalStrike { get; set; } = false;
        public bool Kill { get; set; } = false;
        public int Damage {  get; set; }
    }
}
