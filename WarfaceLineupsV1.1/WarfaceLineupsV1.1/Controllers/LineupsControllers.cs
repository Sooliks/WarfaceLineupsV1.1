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
        if (filterForLineupsData.Search == null) filterForLineupsData.Search = "";
        var lineups = HandlerLineups.GetVerifiedLineups(filterForLineupsData, minId, countVideosOnOnePage);
        return Results.Json(lineups.Select(l => new { Id = l.Id, Title = l.Title, Description = l.Description, IsVerified = l.IsVerified, UrlOnVideo = l.UrlOnVideo, TypeMap = l.TypeMap.Id, TypeSide = l.TypeSide, TypeFeature = l.TypeFeature, TypePlant = l.TypePlant }).ToList());
    }
    [AuthorizeAdminByJwt]
    [HttpGet("api/unverifiedlineups")]
    public async Task<IResult> GetUnVerifiedLineups(int page)
    {
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        var lineups = HandlerLineups.GetUnVerifiedLineups(minId, countVideosOnOnePage);
        return Results.Json(lineups.Select(l => new { Id = l.Id, Title = l.Title, Description = l.Description, IsVerified = l.IsVerified, UrlOnVideo = l.UrlOnVideo, TypeMap = l.TypeMap.Id, TypeSide = l.TypeSide, TypeFeature = l.TypeFeature, TypePlant = l.TypePlant }).ToList());

    }
    [HttpGet("api/verifiedlineupsbyownerid")]
    public async Task<IResult> GetVerifiedLineupsByOwner(int ownerId,int page, FilterForLineupsData filterForLineupsData)
    {
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        if (filterForLineupsData.Search == null) filterForLineupsData.Search = "";
        var lineups = HandlerLineups.GetVerifiedLineupsByOwnerId(filterForLineupsData, minId, countVideosOnOnePage, ownerId);
        return Results.Json(lineups.Select(l => new { Id = l.Id, Title = l.Title, Description = l.Description, IsVerified = l.IsVerified, UrlOnVideo = l.UrlOnVideo, TypeMap = l.TypeMap.Id, TypeSide = l.TypeSide, TypeFeature = l.TypeFeature, TypePlant = l.TypePlant }).ToList());
    }
    [AuthorizeByJwt]
    [HttpGet("api/lineupsbyowner")]
    public async Task<IResult> OnGetVerifiedLineupsByOwner(int page, FilterForLineupsData filterForLineupsData)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        const int countVideosOnOnePage = 8;
        int minId = (page * countVideosOnOnePage) - countVideosOnOnePage;
        if (filterForLineupsData.Search == null) filterForLineupsData.Search = "";
        var lineups =HandlerLineups.GetAllLineupsByOwnerId(filterForLineupsData, minId, countVideosOnOnePage, account.Id);
        return Results.Json(lineups.Select(l => new { Id = l.Id, Title = l.Title, Description = l.Description, IsVerified = l.IsVerified, UrlOnVideo = l.UrlOnVideo, TypeMap = l.TypeMap.Id, TypeSide = l.TypeSide, TypeFeature = l.TypeFeature, TypePlant = l.TypePlant }).ToList());

    }
    [AuthorizeAdminByJwt]
    [HttpDelete("api/deletelineup/admin")]
    public async Task<IResult> DeleteLineup(int lineupId)
    {
        var adminAccount = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        var lineup = HandlerLineups.GetLineupByLineupId(lineupId);
        HandlerNotifications.SendNotify(adminAccount, lineup.Owner, "Lineup отклонен", $"Ваш lineup: {lineup.Title}, был отклонен модерацией, попробуйте опубликовать заного");
        HandlerLineups.DeleteLineup(lineupId);
        return Results.Ok();
    }
    [AuthorizeByJwt]
    [HttpDelete("api/deletelineup/user")]
    public async Task<IResult> DeleteLineupUser(int lineupId)
    {
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        var lineup = HandlerLineups.GetLineupByLineupId(lineupId);
        if (lineup.Owner.Id != account.Id)
        {
            return Results.BadRequest();
        }
        HandlerLineups.DeleteLineup(lineupId);
        return Results.Ok();
    }

    [AuthorizeAdminByJwt]
    [HttpPost("api/publishlineup")]
    public async Task<IResult> ToPublishLineup([FromBody]int lineupId)
    {
        HandlerLineups.ToPublishLineup(lineupId);
        return Results.Ok();
    }

    [HttpGet("api/lineup/{id:int}")]
    public async Task<IResult> GetLineupById(int id)
    {
        return Results.Json(HandlerLineups.GetVerifiedLineupByLineupId(id));
    }

    [AuthorizeByJwt]
    [HttpPost("api/addlineup")]
    public async Task<IResult> AddLineup([FromForm]AddLineupData addLineupData)
    {
        IFormFileCollection files = Request.Form.Files;
        var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
        if (files.Count > 4)
        {
            return Results.BadRequest("Кол-во скринов должно быть не больше 4-х");
        }
        if (files.Count is 0)
        {
            HandlerLineups.AddNewLineup(addLineupData.Title, addLineupData.Description, addLineupData.UrlOnVideo, HandlerMaps.GetMapById(addLineupData.TypeGameMap), addLineupData.TypeSide, addLineupData.TypeFeature, addLineupData.TypePlant, account);
            return Results.Ok();
        }
        else
        {
            HandlerLineups.AddNewLineup(addLineupData.Title, addLineupData.Description, HandlerMaps.GetMapById(addLineupData.TypeGameMap), addLineupData.TypeSide, addLineupData.TypeFeature, addLineupData.TypePlant, files, account);
            return Results.Ok();
        }
    }
}