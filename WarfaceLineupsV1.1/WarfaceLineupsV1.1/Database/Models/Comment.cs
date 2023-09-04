namespace WarfaceLineupsV1._1.Database.Models;

public class Comment
{
    public int Id { get; set; }
    public Lineup Lineup { get; set; }
    public Account Owner { get; set; }
    public string Text { get; set; }
    
    public Comment()
    {
        
    }
    public Comment(string text, Account owner)
    {
        Text = text;
        Owner = owner;
    }
}