/// <summary>
/// Представляет игрока в лабиринте.
/// </summary>
public class Player
{
    public int X { get; private set; }
    public int Y { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса Player.
    /// </summary>
    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

    /// <summary>
    /// Перемещает игрока по лабиринту.
    /// </summary>
    public void Move(int dx, int dy, Maze maze)
    {
        int newX = X + dx;
        int newY = Y + dy;

        if (maze.maze[newY, newX] != '1')
        {
            X = newX;
            Y = newY;
        }
    }
}
