using System;
using System.Collections.Generic;

/// <summary>
/// Представляет собой лабиринт с препятствиями, начальной и конечной точками, а также методом поиска пути.
/// </summary>
public class Maze
{
    private int width;
    private int height;
    public char[,] maze;
    public bool[,] revealed; 
    public int EndX { get; private set; }
    public int EndY { get; private set; }

    /// <summary>
    ///  Инициализирует новый экземпляр класса Maze с указанными размерами.
    /// </summary>
    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;
        revealed = new bool[height, width]; 
        GenerateMaze();
    }

    /// <summary>
    /// Создает схему лабиринта с границами, препятствиями и конечной точкой.
    /// </summary>
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

    /// <summary>
    /// Рисует лабиринт с параметрами видимости и траектории.
    /// </summary>
    /// <param name="playerX">X-координата игрока.</param>
    /// <param name="playerY">Y-координата игрока.</param>
    /// <param name="startX">X-координата начальной точки.</param>
    /// <param name="startY">Y-координата начальной точки.</param>
    /// <param name="path">Список отображаемых координат пути.</param>
    /// <param name="viewRadius">Радиус видимости вокруг игрока.</param>
    public void DrawMaze(int playerX, int playerY, int startX, int startY, List<(int, int)> path, int viewRadius = 5)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (revealed[y, x] || (Math.Abs(playerX - x) <= viewRadius && Math.Abs(playerY - y) <= viewRadius))
                {
                    revealed[y, x] = true;

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
                    else if (path != null && path.Contains((x, y)))
                    {
                        Console.Write('.'); 
                    }
                    else
                    {
                        Console.Write(maze[y, x] == '1' ? '█' : ' ');
                    }
                }
                else
                {
                    Console.Write(' '); 
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine("Нажмите P для показа пути.");
    }

    /// <summary>
    /// Ищет путь от начальной точки до конечной точки с помощью алгоритма поиска в ширину (BFS).
    /// </summary>
    /// <param name="startX">X-координата начальной точки.</param>
    /// <param name="startY">Y-координата начальной точки.</param>
    /// <returns>Список координат, представляющих путь, или null, если путь не найден.</returns>
    public List<(int, int)> FindPath(int startX, int startY)
    {
        bool[,] visited = new bool[height, width];
        Dictionary<(int, int), (int, int)> previous = new Dictionary<(int, int), (int, int)>();
        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startX, startY));
        visited[startY, startX] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int x = current.Item1;
            int y = current.Item2;

            if (x == EndX && y == EndY)
            {
                return ReconstructPath(previous, (startX, startY), (EndX, EndY)); 
            }

            foreach (var direction in new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) }) 
            {
                int newX = x + direction.Item1;
                int newY = y + direction.Item2;

                if (newX >= 0 && newX < width && newY >= 0 && newY < height && !visited[newY, newX] && maze[newY, newX] != '1')
                {
                    queue.Enqueue((newX, newY));
                    visited[newY, newX] = true;
                    previous[(newX, newY)] = (x, y); 
                }
            }
        }

        return null; 
    }

    /// <summary>
    /// Восстанавливает путь от конечной точки к начальной точке, используя словарь предыдущих ячеек.
    /// </summary>
    private List<(int, int)> ReconstructPath(Dictionary<(int, int), (int, int)> previous, (int, int) start, (int, int) end)
    {
        List<(int, int)> path = new List<(int, int)>();
        var current = end;

        while (!current.Equals(start))
        {
            path.Add(current);
            current = previous[current];
        }

        path.Add(start);
        path.Reverse();
        return path;
    }
}
