namespace WarfaceLineupsV1._1.Database.Models;

public class News
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public Account Sender { get; set; }
    
    public News()
    {
        
    }
    
    public News(string title, string text, Account sender)
    {
        Title = title;
        Text = text;
        Sender = sender;
    }
}