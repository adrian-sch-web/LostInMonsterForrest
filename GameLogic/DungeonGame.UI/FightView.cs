using DungeonGame.Core;

namespace DungeonGame.UI
{
    public class FightView
    {
        public void Refresh(Game game, bool risky)
        {
            Monster? monster = game.Map.Monsters.Find(x => x.Id == game.FightMode);
            if (monster is null)
            {
                return;
            }
            Console.WriteLine("\n FIGHT!");
            Console.WriteLine("\nYou" + "  vs  " + monster.Type);
            Console.WriteLine("\n" + game.Map.Player.Hp + "      " + monster.Hp);
            Console.WriteLine("\n\n");
            if (risky)
            {
                Console.WriteLine("Normal Attack    " + "\x1B[4m" + "Risky Attack" + "\x1B[0m");
            }
            else
            {
                Console.WriteLine("\x1B[4m" + "Normal Attack" + "\x1B[0m" + "    Risky Attack");
            }
            foreach (var attack in game.Attacks)
            {
                PrintAttackMessage(attack);
            }
        }

        private void PrintAttackMessage(Attack attack)
        {
            if (attack.Attacker)
            {
                if (attack.Damage == 0)
                {
                    Console.WriteLine("\n\nOh no, your risky Attack missed the monster");
                    return;
                }
                string risky = "normal";
                if (attack.Risky)
                {
                    risky = "risky";
                }
                Console.WriteLine("\n\nYou hit " + attack.Monster.Type + " with a " + risky + " Attack.");
                if (attack.CriticalStrike)
                {
                    Console.WriteLine("It was a critical Strike.");
                }
                Console.WriteLine(attack.Monster.Type + " lost " + attack.Damage + " HP");
                if (attack.Kill)
                {
                    Console.WriteLine(attack.Monster.Type + " died");
                }
            }
            else
            {
                Console.WriteLine("\n\n" + attack.Monster.Type + " hit you.");
                if (attack.CriticalStrike)
                {
                    Console.WriteLine("It was a critical Strike.");
                }
                Console.WriteLine("You lost " + attack.Damage + " HP");
            }

        }
    }
}
