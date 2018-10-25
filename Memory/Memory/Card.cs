//Add in own Card file
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