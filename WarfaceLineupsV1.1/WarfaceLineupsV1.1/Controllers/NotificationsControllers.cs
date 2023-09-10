using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;

namespace WarfaceLineupsV1._1.Controllers;

public class NotificationsControllers : Controller
{
    [AuthorizeByJwt]
    [HttpGet("api/notifications")]
    public async Task<IResult> GetNotifications()
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        return Results.Json(HandlerNotifications.GetNotificationsByRecipientAccount(account));
    }
    [AuthorizeByJwt]
    [HttpDelete("api/deletenotification")]
    public async Task<IResult> DeleteNotification([FromBody]int id)
    {
        try
        {
            var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
            HandlerNotifications.DeleteNotify(account, id);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest();
        }
    }
    [AuthorizeByJwt]
    [HttpGet("api/countnotifications")]
    public async Task<IResult> GetCountNotifications()
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        return Results.Json(HandlerNotifications.GetCountNotificationsOfAccount(account));
    }
}