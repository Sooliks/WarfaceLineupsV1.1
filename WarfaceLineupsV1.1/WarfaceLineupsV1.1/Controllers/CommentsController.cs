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
        return Results.Json(HandlerComments.GetAllCommentsByLineupIdAndPage(addCommentData.IdLineup,1), statusCode: 200);
    }

    [HttpGet("api/comments")]
    public async Task<IResult> GetComments(int lineupId, int page)
    {
        var comments = HandlerComments.GetAllCommentsByLineupIdAndPage(lineupId, page);
        return Results.Json(comments.Select(c=>new {Id = c.Id, OwnerLogin = c.Owner.Login, OwnerId = c.Owner.Id, Text = c.Text}).ToList(), statusCode: 200);
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
    public async Task<IResult> DeleteCommentUser(int id)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsAccountOwnerComment(account, id))
        {
            HandlerComments.DeleteComment(id);
            return Results.Ok();
        }
        return Results.BadRequest();
    }
    [AuthorizeAdminByJwt]
    [HttpDelete("api/deletecomment/admin")]
    public async Task<IResult> DeleteCommentAdmin(int id)
    {
        HandlerComments.DeleteComment(id);
        return Results.Ok();
    }
    
    [AuthorizeByJwt]
    [HttpDelete("api/deletecomment/ownerlineup")]
    public async Task<IResult> DeleteCommentOwnerLineup(int id)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (HandlerComments.IsCommentBelongToAccount(account,id))
        {
            HandlerComments.DeleteComment(id);
            return Results.Ok();
        }

        return Results.BadRequest();
    }
}