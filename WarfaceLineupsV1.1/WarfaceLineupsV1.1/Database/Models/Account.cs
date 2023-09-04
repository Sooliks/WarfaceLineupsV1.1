namespace WarfaceLineupsV1._1.Database.Models;

public class Account
{ 
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsVerifiedAccount { get; set; } 
    public bool IsPremiumAccount { get; set; }
    public List<Lineup> Lineups { get; set; }

    public Account()
    {
        
    }

    public Account(string login, string email, string password)
    {
        this.Login = login;
        this.Email = email;
        this.Password = password;
        this.Role = "member";
        this.IsVerifiedAccount = false;
        this.IsPremiumAccount = false;
        this.Lineups = new List<Lineup>();
    }
}