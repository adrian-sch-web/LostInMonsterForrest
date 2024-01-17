using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    internal class UserInterface
    {
        public string[] board;

        public UserInterface(int[] size)
        {
            board = new string[size[1] + 2];
            board[0] = new string('*', size[0] + 2);
            for(int i = 0; i < size[1]; i++)
            {
                board[i + 1] = "*" + new string(' ', size[0]) + "*";
            }
            board[size[1] + 1] = board[0];
        }
        public void Refresh(Game game)
        {
            Console.Clear();
            Console.WriteLine(String.Concat("HP: " + game.Player.Hp + "    AD: " + game.Player.Damage + "    Crit Chance: " + game.Player.CritChance + "%"));
            if (game.FightMode != -1)
            {
                RefreshFight(game);
            }
            else
            {
                RefreshBoard(game);
            }
        }

        public void RefreshFight(Game game)
        {
            Monster monster = game.Monsters.Find(x => x.id == game.FightMode);
            Console.WriteLine("\n\n\nFIGHT!!!");
            Console.WriteLine("\n\n" + game.Player.Hp + "      " + monster.Hp);

        }
        public void RefreshBoard(Game game)
        {
            string[] tempBoard = new string[board.Length];
            for (int i = 0; i < tempBoard.Length; i++)
            {
                tempBoard[i] = board[i];
            }
            tempBoard[game.Player.Position[1] + 1] =
                string.Concat(tempBoard[game.Player.Position[1] + 1].AsSpan(0, game.Player.Position[0] + 1), 
                "+",
                tempBoard[game.Player.Position[1] + 1].AsSpan(game.Player.Position[0] + 2));

            tempBoard[game.Door.Position[1] + 1] =
                string.Concat(tempBoard[game.Door.Position[1] + 1].AsSpan(0, game.Door.Position[0] + 1),
                "¶",
                tempBoard[game.Door.Position[1] + 1].AsSpan(game.Door.Position[0] + 2));

            foreach ( var monster in game.Monsters)
            {
                tempBoard[monster.Position[1] + 1] =
                    string.Concat(tempBoard[monster.Position[1] + 1].AsSpan(0, monster.Position[0] + 1),
                    monster.Symbol,
                    tempBoard[monster.Position[1] + 1].AsSpan(monster.Position[0] + 2));
            }

            for(int i = 0; i < tempBoard.Length; i++) 
            {
                Console.WriteLine(tempBoard[i]);
            }
        }

        public ConsoleKey WaitUserInput()
        {
            ConsoleKeyInfo keyInfo;
            do
            { 
                keyInfo = Console.ReadKey();
            }
            while(
                keyInfo.Key != ConsoleKey.LeftArrow &&
                keyInfo.Key != ConsoleKey.RightArrow &&
                keyInfo.Key != ConsoleKey.UpArrow &&
                keyInfo.Key != ConsoleKey.DownArrow &&
                keyInfo.Key != ConsoleKey.Enter
            );
            return keyInfo.Key;
        }
    }
}
