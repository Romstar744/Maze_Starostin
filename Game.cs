using System;
using System.Collections.Generic;

/// <summary>
/// Управляет игровым состоянием, перемещениями игроков и взаимодействиями в лабиринте.
/// </summary>
public class Game
{
    private Maze maze;
    private Player player;
    private int startX = 1;
    private int startY = 1;
    private bool showPath = false; 
    private int viewRadius = 5;

    /// <summary>
    /// Инициализирует новый экземпляр игрового класса.
    /// </summary>
    public Game(int width, int height)
    {
        maze = new Maze(width, height);
        player = new Player(startX, startY);
    }

    /// <summary>
    /// Запускает цикл игры, управляя движениями игрока и рисуя лабиринт.
    /// </summary>
    public void Play()
    {
        List<(int, int)> path = maze.FindPath(startX, startY); 

        while (true)
        {
            Console.Clear();
            maze.DrawMaze(player.X, player.Y, startX, startY, showPath ? path : null, viewRadius); 

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    player.Move(0, -1, maze);
                    break;
                case ConsoleKey.DownArrow:
                    player.Move(0, 1, maze);
                    break;
                case ConsoleKey.LeftArrow:
                    player.Move(-1, 0, maze);
                    break;
                case ConsoleKey.RightArrow:
                    player.Move(1, 0, maze);
                    break;
                case ConsoleKey.P:
                    showPath = !showPath; 
                    break;
            }

            if (player.X == maze.EndX && player.Y == maze.EndY)
            {
                Console.Clear();
                Console.WriteLine("Поздравляем! Вы достигли конца лабиринта!");
                break;
            }
        }
    }
}
