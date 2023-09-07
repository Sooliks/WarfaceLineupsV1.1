namespace WarfaceLineupsV1._1.Database.Models;

public class Notification
{
    public int Id { get; set; }
    public Account Sender { get; set; }
    public Account Recipient { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    
    public Notification()
    {
        
    }
    public Notification(Account sender, Account recipient, string title, string text)
    {
        Sender = sender;
        Recipient = recipient;
        Title = title;
        Text = text;
    }
}