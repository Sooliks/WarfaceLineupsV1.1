using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class NewsControllers : Controller
{
    [AuthorizeAdminByJwt]
    [HttpPost("api/publishnews")]
    public async Task<IResult> PublishNews([FromBody]PublishNewsData publishNewsData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        HandlerNews.PublishNews(publishNewsData.Title, publishNewsData.Text,account);
        return Results.Ok();
    }

    [HttpGet("api/news/{filter:int}")]
    public async Task<IResult> GetNews(int filter)
    {
        switch (filter)
        {
            case 1:
                return Results.Json(HandlerNews.GetNewsDescending());
            case 0:
                return Results.Json(HandlerNews.GetNewsAscending());
            default:
                return Results.Json(HandlerNews.GetNewsDescending());
        }
    }
}