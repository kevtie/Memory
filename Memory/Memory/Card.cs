/// <summary>
/// Card is a class where a cards data is stored.
/// </summary>
public class Card
{
    public int Id;
    public int DuplicateId;
    public bool Active;
    public string Title;
    public int Row;
    public int Column;
    public bool Flipped;
    public string FrontBackground;
    public string BackBackground;

    // _flipped set Flipped to { get; set; }

    /// <summary>
    /// Card is a method that gets excecuted when a new Card object is created.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="duplicateId"></param>
    /// <param name="active"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="title"></param>
    /// <param name="flipped"></param>
    /// <param name="frontBackground"></param>
    /// <param name="backBackground"></param>
    public Card(int id, int duplicateId, bool active, int column, int row, string title, bool flipped, string frontBackground, string backBackground)
    {
        Id = id;
        DuplicateId = duplicateId;
        Active = active;
        Title = title;
        Row = row;
        Column = column;
        Flipped = flipped;
        FrontBackground = frontBackground;
        BackBackground = backBackground;
    }
}