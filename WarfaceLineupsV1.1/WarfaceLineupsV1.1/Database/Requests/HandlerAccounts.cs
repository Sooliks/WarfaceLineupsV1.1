using WarfaceLineupsV1._1.Database.Models;
using WfTracker.Utils;

namespace WarfaceLineupsV1._1.Database.Requests;

public static class HandlerAccounts
{
    public static bool IsLoginExist(string login)
    {
        using var db = new Context();
        return db.Accounts.SingleOrDefault(a => a.Login == login) != null;
    }
    public static bool IsEmailExist(string email)
    {
        using var db = new Context();
        return db.Accounts.SingleOrDefault(a => a.Email == email) != null;
    }
    public static bool IsPasswordValid(string email, string password)
    {
        var activeAccount = new Account();
        using (Context db = new Context())
        {
            activeAccount = db.Accounts.SingleOrDefault(a => a.Email == email);
        }

        if (Bcrypt.BCrypt.CheckPassword(password, activeAccount.Password)) return true;
        return false;
    }
    public static bool IsPasswordValid(Account account, string password)
    {
        if (Bcrypt.BCrypt.CheckPassword(password, account.Password)) return true;
        return false;
    }
    public static Account Register(string login, string email, string password)
    {
        using (Context db = new Context())
        {
            string saltePassword = Bcrypt.BCrypt.HashPassword(password, Bcrypt.BCrypt.GenerateSalt());
            var account = new Account(login, email, saltePassword);
            db.Accounts.Add(account);
            db.SaveChanges();
            return db.Accounts.FirstOrDefault(a=>a.Email == account.Email);
        }
    }
    public static List<Comment> GetComments(Account account)
    {
        using Context db = new Context();
        account = db.Accounts.SingleOrDefault(a => a == account);
        db.Entry(account).Collection(c=>c.Comments).Load();
        return account.Comments;
    }
    public static List<Lineup> GetAllLineups(Account account)
    {
        using Context db = new Context();
        account = db.Accounts.SingleOrDefault(a => a == account);
        db.Entry(account).Collection(c=>c.Lineups).Load();
        return account.Lineups;
    }
    public static List<Lineup> GetVerifiedLineups(Account account)
    {
        using Context db = new Context();
        account = db.Accounts.SingleOrDefault(a => a == account);
        db.Entry(account).Collection(c=>c.Lineups).Load();
        return account.Lineups.Where(l => l.IsVerified).ToList();
    }
    public static Account GetAccountByEmail(string email)
    {
        using Context db = new Context();
        var account = db.Accounts.FirstOrDefault(a => a.Email == email);
        return account;
    }
    public static Account GetAccountByLogin(string login)
    {
        using Context db = new Context();
        var account = db.Accounts.FirstOrDefault(a => a.Login == login);
        return account;
    }
    public static string GenerateVerificationCodeForAccount(Account account)
    {
        using Context db = new Context();
        if (account != null)
        {
            var verificationCode = AuthService.GenerateVerificationCode();
            account.VerificationCode = verificationCode;
            db.Accounts.Update(account);
            db.SaveChangesAsync();
            return verificationCode;
        }
        return "";
    }
    public static bool CheckIsValidVerificationCodeForAccount(Account account, string verificationCode)
    {
        using Context db = new Context();
        if (account.VerificationCode == verificationCode)
        {
            account.IsVerifiedAccount = true;
            account.VerificationCode = "";
            db.Accounts.Update(account);
            db.SaveChangesAsync();
            return true;
        }
        return false;
    }
}