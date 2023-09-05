using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public static class HandlerComments
{
    public static void AddNewComment(Account owner, int lineupId, string text)
    {
        using Context db = new Context();
        var comment = new Comment(text,owner,HandlerLineups.GetLineupByLineupId(lineupId));
        db.Comments.Add(comment);
        db.SaveChangesAsync();
    }
    public static void DeleteComment(int idComment)
    {
        using Context db = new Context();
        var comment = db.Comments.SingleOrDefault(c=>c.Id==idComment);
        db.Comments.Remove(comment);
        db.SaveChanges();
    }
    public static void UpdateComment(int commentId, string newComment)
    {
        using Context db = new Context();
        var comment = db.Comments.SingleOrDefault(c => c.Id == commentId);
        comment.Text = newComment;
        db.Comments.Update(comment);
        db.SaveChanges();
    }
    public static bool IsAccountOwnerComment(Account account, int commentId)
    {
        using Context db = new Context();
        var comment = db.Comments.SingleOrDefault(c => c.Id == commentId);
        if (comment.Owner.Id == account.Id) return true;
        
        return false;
    }
    public static bool IsCommentBelongToAccount(Account account, int commentId)
    {
        using Context db = new Context();
        var comment = db.Comments.SingleOrDefault(c => c.Id == commentId);
        if (comment.Lineup.Owner.Id == account.Id)
        {
            return true;
        }
        return false;
    }
}