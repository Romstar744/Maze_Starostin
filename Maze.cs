using System;

public class Maze
{
    private int width;
    private int height;
    public char[,] maze;
    public int EndX { get; private set; }
    public int EndY { get; private set; }

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        maze = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                maze[y, x] = (x == 0 || y == 0 || x == width - 1 || y == height - 1) ? '1' : '0';
            }
        }

        Random rand = new Random();
        int numObstacles = (width * height) / 5; 
        for (int i = 0; i < numObstacles; i++)
        {
            int obstacleX = rand.Next(1, width - 2);
            int obstacleY = rand.Next(1, height - 2);
            maze[obstacleY, obstacleX] = '1'; 
        }

        EndX = rand.Next(1, width - 2);
        EndY = rand.Next(1, height - 2);
        maze[EndY, EndX] = 'E'; 
    }

    public void DrawMaze(int playerX, int playerY, int startX, int startY, bool showPath)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == playerX && y == playerY)
                {
                    Console.Write('P'); 
                }
                else if (x == startX && y == startY)
                {
                    Console.Write('S'); 
                }
                else if (x == EndX && y == EndY)
                {
                    Console.Write('E'); 
                }
                else if (showPath && maze[y, x] == '0')
                {
                    Console.Write('.'); 
                }
                else
                {
                    Console.Write(maze[y, x] == '1' ? '#' : ' '); 
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine("Нажмите P для показа пути.");
    }
}
