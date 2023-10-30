using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper2
{
    public class Board
    {
        Program program = new Program();

        public static void DrawBoard()
        {
            int rows = Program.userSize, columns = Program.userSize, mineCount = Program.userDifficulty;
            int[,] minefield = new int[rows, columns];


            for (int k = 0; k < mineCount; k++)
            {
                int y = new Random().Next(rows);
                int x = new Random().Next(columns);
                while (minefield[y, x] == 9)
                {
                    y = new Random().Next(rows);
                    x = new Random().Next(columns);
                }
                minefield[y, x] = 9;

                for (int i = Math.Max(y - 1, 0); i <= Math.Min(y + 1, rows - 1); i++)
                    for (int j = Math.Max(x - 1, 0); j <= Math.Min(x + 1, columns - 1); j++)
                        if (minefield[i, j] != 9)
                            minefield[i, j]++;
            }


            DrawMinefield(minefield);


            //loop start
            GameResult result = GameResult.Continue;
            while (result == GameResult.Continue)
            {
                // Get coordinates from player
                int y, x;
                while (true)
                    try
                    {
                        Console.Write("Y: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        Console.Write("X: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        if (x >= 0 && y >= 0 && x < columns && y < rows) // Minefield limits
                            if (minefield[y, x] >= 0)                    // Covered field
                                break;                                   // OK
                            else
                                Console.WriteLine("This field is already uncovered");
                        else
                            Console.WriteLine("Invalid coordinates");
                    }
                    catch
                    {
                        Console.WriteLine("Coordinates must be a valid integer");
                    }

                // Uncover the field by type
                if (minefield[y, x] == 9) // Mine
                {
                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < columns; j++)
                            if (minefield[i, j] >= 1 && minefield[i, j] <= 9)
                                minefield[i, j] *= -1;
                            else if (minefield[i, j] == 0)
                                minefield[i, j] = -10;
                    result = GameResult.Loss;
                }
                else if (minefield[y, x] >= 0) // 0-8
                    UncoverField(y, x, minefield);

                // Verification of a win (everything except mines is uncovered)
                if (result == GameResult.Continue)
                {
                    result = GameResult.Win;
                    foreach (int field in minefield)
                        if (field >= 0 && field != 9)
                        {
                            result = GameResult.Continue;
                            break;
                        }
                }

                DrawMinefield(minefield);
            }

            if (result == GameResult.Loss)
                Console.WriteLine("You lose!");
            else
                Console.WriteLine("You win!");
        }

        private static void UncoverField(int i, int j, int[,] minefield)
        {
            if (i >= 0 && j >= 0 &&
                i < minefield.GetLength(0) && j < minefield.GetLength(1))
            {
                if (minefield[i, j] == 0)
                {
                    minefield[i, j] = -10;
                    // Uncover all nearby fields
                    for (int y = -1; y <= 1; y++)
                        for (int x = -1; x <= 1; x++)
                            UncoverField(i + y, j + x, minefield);
                }
                else if (minefield[i, j] > 0 && minefield[i, j] < 9) // 1-8
                    minefield[i, j] *= -1;
            }
        }



        static void DrawMinefield(int[,] minefield)
        {
            Console.Clear();
            Console.Write("   | ");
            for (int j = 0; j < minefield.GetLength(1); j++)
                Console.Write("{0,2} ", j);
            Console.WriteLine();
            Console.WriteLine("---+-".PadRight(minefield.GetLength(1) * 3 + 5, '-'));

            for (int i = 0; i < minefield.GetLength(0); i++)
            {
                Console.Write("{0,2} | ", i);
                for (int j = 0; j < minefield.GetLength(1); j++)
                {
                    if (minefield[i, j] >= 0) // Covered field
                        WriteColorizedChar('#', ConsoleColor.Black);
                    //else if (minefield[i, j] == -10) // Uncovered empty field
                    //    WriteColorizedChar('~', ConsoleColor.DarkGray);
                    else if (minefield[i, j] == -9) // Uncovered mine
                        WriteColorizedChar('X', ConsoleColor.Gray);
                    else // Uncovered number
                        WriteColorizedChar((-minefield[i, j]).ToString()[0],
                            ConsoleColor.DarkCyan);

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void WriteColorizedChar(char letter, ConsoleColor color)
        {
            var originalColor = Console.BackgroundColor;
            Console.BackgroundColor = originalColor;
            Console.Write(" {0} ", letter);
            Console.BackgroundColor = originalColor;
        }

        private class GameResult
        {
            internal static GameResult Continue { get; set; }

            public static GameResult Loss { get; set; }
            public static GameResult Win { get; set; }
        }
    }

}




//for (int i = 0; i < minefield.GetLength(0); i++)
//{
//    Console.Write("{0,2} | ", i);
//    for (int j = 0; j < minefield.GetLength(1); j++)
//    {
//        if (minefield[i, j] >= 0) // Covered field
//            WriteColorizedChar('#', ConsoleColor.Black);
//        else if (minefield[i, j] == -10) // Uncovered empty field
//            WriteColorizedChar('~', ConsoleColor.DarkGray);
//        else if (minefield[i, j] == -9) // Uncovered mine
//            WriteColorizedChar('X', ConsoleColor.Red);
//        else // Uncovered number
//            WriteColorizedChar((-minefield[i, j]).ToString()[0],
//                ConsoleColor.DarkCyan);
//
//    }
//    Console.WriteLine();
//}
//Console.WriteLine();