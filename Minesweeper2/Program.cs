using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper2
{
    class Program
    {
        public static int userSize;
        public static int userDifficulty;

        public static void Main(string[] args)
        {
            int difficultyCheck = 0;
            Console.WriteLine("Enter the size of the board. eg:'10'= 10x10 board");
            userSize = int.Parse(Console.ReadLine());

            Console.WriteLine("difficulty? (1)easy (2)hard (3)insane");
            difficultyCheck = int.Parse(Console.ReadLine()) - 1;

            if (difficultyCheck == 0)
            {
                userDifficulty = userSize * userSize / 6;
            }
            if (difficultyCheck == 1)
            {
                userDifficulty = userSize * userSize / 4;
            }
            if (difficultyCheck == 2)
            {
                userDifficulty = userSize * userSize / 2;
            }

            Board.DrawBoard();
        }


    }
}