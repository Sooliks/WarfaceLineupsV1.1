using WarfaceLineupsV1._1.Database.Requests;

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
    public string VerificationCode { get; set; }
    public string JwtToken { get; set; }
    public byte[] Avatar { get; set; }
    public List<Lineup> Lineups { get; set; }
    public List<Comment> Comments { get; set; }

    public Account()
    {
        
    }

    public Account(string login, string email, string password, string jwtToken)
    {
        this.Login = login;
        this.Email = email;
        this.Password = password;
        this.Role = "member";
        this.IsVerifiedAccount = false;
        this.IsPremiumAccount = false;
        this.VerificationCode = "";
        this.JwtToken = jwtToken;
        this.Avatar = new byte[] { };
        this.Lineups = new List<Lineup>();
        this.Comments = new List<Comment>();
    }
    
    public static List<Comment> GetComments(Account account) => HandlerAccounts.GetComments(account);
    public static List<Lineup> GetAllLineups(Account account) => HandlerAccounts.GetAllLineups(account);
    public static List<Lineup> GetVerifiedLineups(Account account) => HandlerAccounts.GetVerifiedLineups(account);
}