using DungeonGame.Core;

namespace DungeonGame.UI
{
    internal class UserInterface(Position _size)
    {
        private readonly BoardView board = new(_size);
        private readonly FightView fight = new();
        private bool riskyAttack;

        public void Refresh(Game game)
        {
            Console.Clear();
            if (!game.IsRunning)
            {
                Death(game);
                return;
            }
            Console.WriteLine(String.Concat("HP: " + game.Map.Player.Hp + "    AD: " + game.Map.Player.Damage + "    Crit Chance: " + game.Map.Player.CritChance + "%"));
            if (game.FightMode != -1)
            {
                fight.Refresh(game, riskyAttack);
            }
            else
            {
                board.Refresh(game);
            }
        }

        public Input WaitUserInput(Game game)
        {
            ConsoleKeyInfo keyInfo;
            if (game.FightMode != -1)
            {
                do
                {
                    keyInfo = Console.ReadKey();
                }
                while (
                    keyInfo.Key != ConsoleKey.LeftArrow &&
                    keyInfo.Key != ConsoleKey.RightArrow &&
                    keyInfo.Key != ConsoleKey.Enter
                );
                if(keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) 
                {
                    riskyAttack = !riskyAttack;
                    Refresh(game);
                    return WaitUserInput(game);
                }
                if(riskyAttack)
                {
                    return Input.RiskyAttack;
                }
                return Input.NormalAttack;
            }
            
            
            do
            { 
                keyInfo = Console.ReadKey();
            }
            while(
                keyInfo.Key != ConsoleKey.LeftArrow &&
                keyInfo.Key != ConsoleKey.RightArrow &&
                keyInfo.Key != ConsoleKey.UpArrow &&
                keyInfo.Key != ConsoleKey.DownArrow
            );
            if (!MoveCheck(game, keyInfo))
            {
                return WaitUserInput(game);
            }
            return KeyToInput(keyInfo.Key);
        }

        private bool MoveCheck(Game game, ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (game.Map.Player.Position.Y == 0)
                        return false;
                    break;
                case ConsoleKey.RightArrow:
                    if (game.Map.Player.Position.Y == Map.Size.Y)
                        return false;
                    break;
                case ConsoleKey.UpArrow:
                    if (game.Map.Player.Position.X == 0)
                        return false;
                    break;
                case ConsoleKey.DownArrow:
                    if (game.Map.Player.Position.X == Map.Size.X)
                        return false;
                    break;
                default: 
                    return false;
            }
            return true;
        }

        private void Death(Game game)
        {
            Console.Clear();
            Console.WriteLine("\n\nGame Over!\n\n\n");
            Console.WriteLine("You died and made it to Floor " + game.Stats.Floor + "!");
            Console.WriteLine("On your way through the dungeon you made " + game.Stats.Steps + " steps.");
            Console.WriteLine(game.Stats.Kills + " cruel Monsters were defeated by you!");
        }

        private Input KeyToInput(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.LeftArrow => Input.Left,
                ConsoleKey.RightArrow => Input.Right,
                ConsoleKey.UpArrow => Input.Up,
                ConsoleKey.DownArrow => Input.Down,
                _ => throw new Exception("Invalid Key"),
            };
        }
    }
}
