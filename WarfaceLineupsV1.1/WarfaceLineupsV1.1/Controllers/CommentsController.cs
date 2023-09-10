using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class CommentsController : Controller
{
    [AuthorizeByJwt]
    [HttpPost("api/addcomment")]
    public async Task<IResult> AddNewComment([FromBody]AddCommentData addCommentData)
    {
        var login = Request.Headers["login"];
        var account = HandlerAccounts.GetAccountByLogin(login);
        HandlerComments.AddNewComment(account,addCommentData.IdLineup,addCommentData.Text);
        return Results.Json(new {message = "success", comments = HandlerComments.GetAllCommentsByLineupIdAndPage(addCommentData.IdLineup,1)});
    }

    [HttpGet("api/comments")]
    public async Task<IResult> GetComments(int lineupId, int page)
    {
        return Results.Json(HandlerComments.GetAllCommentsByLineupIdAndPage(lineupId, page));
    }
    
    [AuthorizeByJwt]
    [HttpPost("api/updatecomment")]
    public async Task<IResult> UpdateComment([FromBody]UpdateCommentData updateCommentData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsAccountOwnerComment(account, updateCommentData.IdComment))
        {
            HandlerComments.UpdateComment(updateCommentData.IdComment, updateCommentData.NewText);
            return Results.Ok();
        }
        return Results.BadRequest();
    }
    
    [AuthorizeByJwt]
    [HttpDelete("api/deletecomment/user")]
    public async Task<IResult> DeleteCommentUser([FromBody]DeleteCommentData deleteCommentData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsAccountOwnerComment(account, deleteCommentData.IdComment))
        {
            HandlerComments.DeleteComment(deleteCommentData.IdComment);
            return Results.Ok();
        }
        return Results.BadRequest();
    }
    [AuthorizeAdminByJwt]
    [HttpDelete("api/deletecomment/admin")]
    public async Task<IResult> DeleteCommentAdmin([FromBody]DeleteCommentData deleteCommentData)
    {
        HandlerComments.DeleteComment(deleteCommentData.IdComment);
        return Results.Ok();
    }
    
    [AuthorizeByJwt]
    [HttpDelete("api/deletecomment/ownerlineup")]
    public async Task<IResult> DeleteCommentOwnerLineup([FromBody]DeleteCommentData deleteCommentData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsCommentBelongToAccount(account,deleteCommentData.IdComment))
        {
            HandlerComments.DeleteComment(deleteCommentData.IdComment);
            return Results.Ok();
        }

        return Results.BadRequest();
    }
}