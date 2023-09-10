using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;

namespace WarfaceLineupsV1._1.Controllers;

public class CommentsController : Controller
{
    [AuthorizeByJwt]
    [HttpPost("api/addcomment")]
    public async Task<IResult> AddNewComment([FromBody]int idLineup, [FromBody]string text)
    {
        var login = Request.Headers["login"];
        var account = HandlerAccounts.GetAccountByLogin(login);
        HandlerComments.AddNewComment(account,idLineup,text);
        return Results.Json(new {message = "success", comments = HandlerComments.GetAllCommentsByLineupIdAndPage(idLineup,1)});
    }

    [HttpGet("api/comments")]
    public async Task<IResult> GetComments(int lineupId, int page)
    {
        return Results.Json(HandlerComments.GetAllCommentsByLineupIdAndPage(lineupId, page));
    }
    
    [AuthorizeByJwt]
    [HttpPost("api/updatecomment")]
    public async Task<IResult> UpdateComment([FromBody]int idComment, [FromBody]string newComment)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsAccountOwnerComment(account, idComment))
        {
            HandlerComments.UpdateComment(idComment, newComment);
            return Results.Ok();
        }
        return Results.BadRequest();
    }
    
    [AuthorizeByJwt]
    [HttpDelete("api/deletecomment/user")]
    public async Task<IResult> DeleteCommentUser([FromBody]int idComment)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsAccountOwnerComment(account, idComment))
        {
            HandlerComments.DeleteComment(idComment);
            return Results.Ok();
        }
        return Results.BadRequest();
    }
    [AuthorizeAdminByJwt]
    [HttpPost("api/deletecomment/admin")]
    public async Task<IResult> DeleteCommentAdmin([FromBody]int idComment)
    {
        HandlerComments.DeleteComment(idComment);
        return Results.Ok();
    }
    
    [AuthorizeByJwt]
    [HttpPost("api/deletecomment/ownerlineup")]
    public async Task<IResult> DeleteCommentOwnerLineup([FromBody]int idComment)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsCommentBelongToAccount(account,idComment))
        {
            HandlerComments.DeleteComment(idComment);
            return Results.Ok();
        }

        return Results.BadRequest();
    }
}