/// <summary>
/// Player class is a class where a players data is stored.
/// </summary>
public class Player
{
    public int Id;
    public bool Turn;
    public int Score;
    public string Name;
    //public string Status;

    /// <summary>
    /// Player is a method that gets excecuted when a new Player object is created.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="turn"></param>
    /// <param name="score"></param>
    /// <param name="name"></param>
    public Player(int id, bool turn, int score, string name) //string status
    {
        Id = id;
        Turn = turn;
        Score = score;
        Name = name;
        //Status = status;
    }
}