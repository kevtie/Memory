/// <summary>
/// Background is a class where a backgrounds data is stored.
/// </summary>
public class Background
{
    public int Id;
    public string Front;
    public string Back;

    /// <summary>
    /// Background is a method that gets excecuted when a new Background object is created.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <param name="back">Card back image path.</param>
    /// <param name="front">Card front image path.</param>
    public Background(int id, string back, string front)
    {
        Id = id;
        Front = front;
        Back = back;
    }
}