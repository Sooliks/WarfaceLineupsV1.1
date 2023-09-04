namespace WarfaceLineupsV1._1.Database.Models;

public class Lineup
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsVerified { get; set; }
    
    public Account Account { get; set; }
    public List<Comment> Comments { get; set; }

    public Lineup()
    {
        
    }
    public Lineup(string title, string description, Account account)
    {
        this.Title = title;
        this.Description = description;
        this.Account = account;
    }
}