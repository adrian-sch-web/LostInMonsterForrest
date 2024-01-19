﻿using System;
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
            if (!game.IsRunning)
            {
                Death(game);
                return;
            }
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

        private void RefreshFight(Game game)
        {
            Monster monster = game.Monsters.Find(x => x.id == game.FightMode);
            Console.WriteLine("\n\n\nFIGHT!!!");
            Console.WriteLine("\n\n" + game.Player.Hp + "      " + monster.Hp);
            Console.WriteLine("\n\n" + game.Messages);

        }
        private void RefreshBoard(Game game)
        {
            string[] tempBoard = new string[board.Length];
            for (int i = 0; i < tempBoard.Length; i++)
            {
                tempBoard[i] = board[i];
            }
            string floor = "Floor " + game.Floor;
            //tempBoard[0] = String.Concat("*" , floor, tempBoard[0].AsSpan(floor.Length + 1));

            tempBoard[game.Door.Position[1] + 1] = PrintSymbol(game.Door.Position, tempBoard[game.Door.Position[1] + 1], "¶");
            tempBoard[game.Item.Position[1] + 1] = PrintSymbol(game.Item.Position, tempBoard[game.Item.Position[1] + 1], game.Item.Type);
            tempBoard[game.Player.Position[1] + 1] = PrintSymbol(game.Player.Position, tempBoard[game.Player.Position[1] + 1], "+");

            foreach ( var monster in game.Monsters)
            {
                tempBoard[monster.Position[1] + 1] = PrintSymbol(monster.Position, tempBoard[monster.Position[1] + 1], getMonsterSymbol(monster.Type));
            }

            for(int i = 0; i < tempBoard.Length; i++) 
            {
                Console.WriteLine(tempBoard[i]);
            }
            Console.WriteLine("\n\n" + game.Messages);
        }

        private string getMonsterSymbol(int type)
        {
            switch (type)
            {
                case 0:
                    return "§";
                case 1:
                    return "$";
                default:
                    return "#";
            }
        }
        private string PrintSymbol(int[] position, string line,  string symbol)
        {
            line = string.Concat(line.AsSpan(0, position[0] + 1), symbol, line.AsSpan(position[0] + 2));
            return line;
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

        private void Death(Game game)
        {
            Console.Clear();
            Console.WriteLine("\n\nGame Over!\n\n\n");
            Console.WriteLine("You died and made it to Floor " + game.Floor + "!");
        }
    }
}
