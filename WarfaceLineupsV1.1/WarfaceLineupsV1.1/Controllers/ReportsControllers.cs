using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class ReportsControllers : Controller
{
    [AuthorizeByJwt]
    [HttpPost("api/addreport")]
    public async Task<IResult> AddReport(AddReportData addReportData)
    {
        try
        {
            var account = HandlerAccounts.GetAccountByLogin(Request.Headers["login"]);
            var lineup = HandlerLineups.GetLineupByLineupId(addReportData.LineupId);
            if (lineup.Owner.Id == account.Id)
            {
                return Results.BadRequest("Это ваш лайнап!");
            }

            if (HandlerReports.AddNewReport(account, lineup, addReportData.TypeReport))
            {
                return Results.Ok();
            }

            return Results.BadRequest("Вы уже отправляли репорт на этот лайнап!");
        }
        catch (Exception e)
        {
            return Results.BadRequest();
        }
    }
    [AuthorizeAdminByJwt]
    [HttpGet("api/reports")]
    public async Task<IResult> GetReports()
    {
        return Results.Json(HandlerReports.GetExpectedReportsList());
    }
    [AuthorizeAdminByJwt]
    [HttpPost("api/setcompletereport")]
    public async Task<IResult> SetCompleteReport([FromBody]int reportId)
    {
        HandlerReports.SetReportComplete(reportId);
        return Results.Ok();
    }
}