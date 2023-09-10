using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class LineupsControllers : Controller
{
    [HttpGet("api/lineups")]
    public async Task<IResult> GetLineups(int page, FilterForLineupsData filterForLineupsData)
    {
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        return Results.Json(HandlerLineups.GetVerifiedLineups(filterForLineupsData, minId, countVideosOnOnePage));
    }
    [AuthorizeAdminByJwt]
    [HttpGet("api/unverifiedlineups")]
    public async Task<IResult> GetUnVerifiedLineups(int page, FilterForLineupsData filterForLineupsData)
    {
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        return Results.Json(HandlerLineups.GetUnVerifiedLineups(filterForLineupsData, minId, countVideosOnOnePage));
    }
    [HttpGet("api/verifiedlineupsbyownerid")]
    public async Task<IResult> GetVerifiedLineupsByOwner(int ownerId,int page, FilterForLineupsData filterForLineupsData)
    {
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        return Results.Json(HandlerLineups.GetVerifiedLineupsByOwnerId(filterForLineupsData, minId, countVideosOnOnePage, ownerId));
    }
    [AuthorizeByJwt]
    [HttpGet("api/lineupsbyowner")]
    public async Task<IResult> OnGetVerifiedLineupsByOwner(int page, FilterForLineupsData filterForLineupsData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        return Results.Json(HandlerLineups.GetAllLineupsByOwnerId(filterForLineupsData, minId, countVideosOnOnePage, account.Id));
    }
    [AuthorizeAdminByJwt]
    [HttpDelete("api/deletelineup/admin")]
    public async Task<IResult> DeleteLineup([FromBody]int lineupId)
    {
        var adminAccount = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        var lineup = HandlerLineups.GetLineupByLineupId(lineupId);
        HandlerNotifications.SendNotify(adminAccount, lineup.Owner, "Lineup отклонен", $"Ваш lineup: {lineup.Title}, был отклонен модерацией, попробуйте опубликовать заного");
        HandlerLineups.DeleteLineup(lineupId);
        return Results.Ok();
    }
}