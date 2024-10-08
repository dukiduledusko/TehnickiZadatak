using System;
using System.Collections.Generic;

namespace Minesweeper
{
    class Program
    {
        static int boardSize = 8;  
        static int lives = 3;      
        static int moves = 0;      
        static bool[,] mines = new bool[boardSize, boardSize];
        static (int row, int col) playerPosition = (7, 4);  
        static Random random = new Random();
        static List<string> messages = new List<string>(); 

        static void Main(string[] args)
        {
            
            GenerateMines();

            
            while (lives > 0 && playerPosition.row > 0)
            {
                Console.WriteLine("Dobrodosli!");
                Console.Clear();
                DisplayBoard();
                DisplayMessages();
                Console.WriteLine($"Zivota: {lives}, Poteza: {moves}");
                Console.WriteLine("Kuda zelite da idete? (gore, dole, levo, desno): ");
                string move = Console.ReadLine().ToLower();

                
                if (MovePlayer(move))
                {
                    moves++;
                    if (mines[playerPosition.row, playerPosition.col])
                    {
                        messages.Add("Pogodili ste minu! Izgubljen 1 zivot.");
                        lives--;
                    }

                    if (playerPosition.row == 0)
                    {
                        if(mines[playerPosition.row, playerPosition.col])
                        {
                            messages.Add("Pogodili ste minu! Izgubljen 1 zivot.");
                            lives--;
                        }
                        else
                        {
                            messages.Add($"Cestitke! Stigli ste do kraja u {moves} poteza.");
                            break;
                        }
                        
                    }
                }
                else
                {
                    messages.Add("Pokusajte opet!");
                }
            }

            if (lives <= 0)
            {
                messages.Add("Game over! Nemate vise zivota.");
            }
            Console.Clear();
            DisplayBoard();
            DisplayMessages();
        }

        
        static void GenerateMines()
        {
            int mineCount = 10;  
            for (int i = 0; i < mineCount; i++)
            {
                int row = random.Next(0, boardSize);
                int col = random.Next(0, boardSize);
                if (mines[row, col] || (row == 7 && col == 4))  
                {
                    i--;
                }
                else
                {
                    mines[row, col] = true;
                }
            }
        }

        
        static void DisplayBoard()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (row == playerPosition.row && col == playerPosition.col)
                    {
                        Console.Write("P ");  
                    }
                    else
                    {
                        Console.Write(". ");  
                    }
                }
                Console.WriteLine();
            }
        }

        
        static void DisplayMessages()
        {
            Console.WriteLine("\nObavestenja:");
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }

        
        static bool MovePlayer(string direction)
        {
            int newRow = playerPosition.row;
            int newCol = playerPosition.col;

            switch (direction)
            {
                case "gore":
                    if (newRow > 0)
                        newRow--;
                    else
                    {
                        messages.Add("Stop! Ne mozes vise gore!");
                        return false;
                    }
                    break;
                case "dole":
                    if (newRow < boardSize - 1)
                        newRow++;
                    else
                    {
                        messages.Add("Stop! Ne mozes vise dole!");
                        return false;
                    }
                    break;
                case "levo":
                    if (newCol > 0)
                        newCol--;
                    else
                    {
                        messages.Add("Stop! Ne mozes vise levo!");
                        return false;
                    }
                    break;
                case "desno":
                    if (newCol < boardSize - 1)
                        newCol++;
                    else
                    {
                        messages.Add("Stop! Ne mozes vise desno!");
                        return false;
                    }
                    break;
                default:
                    messages.Add("Unesi ispravno potez (gore, dole, levo, desno)!");
                    return false;
            }

            playerPosition = (newRow, newCol);
            return true;
        }
    }
}
