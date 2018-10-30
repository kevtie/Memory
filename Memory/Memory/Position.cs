/// <summary>
/// Position is a class where a positions data is stored.
/// </summary>
public class Position
{
    public int X;
    public int Y;

    /// <summary>
    /// Position is a method that gets excecuted when a new Position object is created.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}