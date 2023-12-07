using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();

        Pixel hoofd = new Pixel();
        hoofd.xPos = screenwidth / 2;
        hoofd.yPos = screenheight / 2;
        hoofd.schermKleur = ConsoleColor.Red;

        string movement = "RIGHT";
        List<int> teljePositie = new List<int>();
        teljePositie.Add(hoofd.xPos);
        teljePositie.Add(hoofd.yPos);

        DateTime tijd = DateTime.Now;
        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth);
        int obstacleYpos = randomnummer.Next(1, screenheight);
        int score = 0;

        while (true)
        {
            Console.Clear();

            //Draw Obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

            //Draw Snake
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);
            Console.Write("■");

            //Draw Borders
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            //Display Score
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Score: " + score);

            //Game Logic
            ConsoleKeyInfo info = Console.ReadKey();
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    movement = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    movement = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    movement = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    movement = "RIGHT";
                    break;
            }

            if (movement == "UP")
                hoofd.yPos--;
            if (movement == "DOWN")
                hoofd.yPos++;
            if (movement == "LEFT")
                hoofd.xPos--;
            if (movement == "RIGHT")
                hoofd.xPos++;

            //Obstacle Collision
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth);
                obstacleYpos = randomnummer.Next(1, screenheight);
            }

            //Collision with Walls or itself
            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)
            {
                GameOver(score, screenwidth, screenheight);
            }

            for (int i = 0; i < teljePositie.Count(); i += 2)
            {
                if (hoofd.xPos == teljePositie[i] && hoofd.yPos == teljePositie[i + 1])
                {
                    GameOver(score, screenwidth, screenheight);
                }
            }

            Thread.Sleep(50);
        }
    }

    static void GameOver(int score, int screenwidth, int screenheight)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
        Console.WriteLine("Gra skończona");
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
        Console.WriteLine("Twój wynik to : " + score);
        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
        Environment.Exit(0);
    }
}

public class Pixel
{
    public int xPos { get; set; }
    public int yPos { get; set; }
    public ConsoleColor schermKleur { get; set; }
    public string character { get; set; }
}
