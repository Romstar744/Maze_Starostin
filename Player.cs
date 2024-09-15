public class Player
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

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
