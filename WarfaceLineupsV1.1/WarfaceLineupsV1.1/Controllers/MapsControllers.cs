
using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class MapsControllers : Controller
{
    [AuthorizeAdminByJwt]
    [HttpPost("api/addmap")]
    public async Task<IResult> AddMap([FromBody]string name)
    {
        HandlerMaps.AddMap(name);
        return Results.Ok();
    }
    [AuthorizeAdminByJwt]
    [HttpPost("api/updatemap")]
    public async Task<IResult> AddMap([FromBody]UpdateMapData updateMapData)
    {
        HandlerMaps.UpdateMap(updateMapData.Id, updateMapData.Name);
        return Results.Ok();
    }

    [HttpGet("api/maps")]
    public async Task<IResult> GetMaps()
    {
        return Results.Json(HandlerMaps.GetAllMaps());
    }
}