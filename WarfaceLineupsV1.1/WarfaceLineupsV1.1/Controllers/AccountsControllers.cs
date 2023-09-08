using Microsoft.AspNetCore.Mvc;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Controllers;

public class AccountsControllers : Controller
{
    [HttpPost("api/registration")]
    public async Task<IResult> OnRegistration(RegistrationData registrationData)
    {
        return Results.Ok();
    }
    [HttpPost("api/authorization")]
    public async Task<IResult> OnAuthorization(AuthorizationData authorizationData)
    {
        return Results.Ok();
    }

    [HttpPost("api/authorizationbyjwt")]
    public async Task<IResult> OnAuthorizationByJwt(AuthorizationByJwtData authorizationByJwtData)
    {
        return Results.Ok();
    }

    [AuthorizeByJwt]
    [HttpGet("api/getverificationcode")]
    public async Task<IResult>  GetVerificationCode()
    {
        return Results.BadRequest();
    }
}