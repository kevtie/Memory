public class Player
{
    public int Id;
    public bool Turn;
    public int Score;
    public string Name;

    public Player(int id, bool turn, int score, string name)
    {
        Id = id;
        Turn = turn;
        Score = score;
        Name = name;
    }
}