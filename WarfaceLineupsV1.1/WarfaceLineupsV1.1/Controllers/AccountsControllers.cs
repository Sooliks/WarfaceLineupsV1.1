using Microsoft.AspNetCore.Mvc;
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
    public async Task<IResult> AuthorizationByJwt(AuthorizationByJwtData authorizationByJwtData)
    {
        return Results.Ok();
    }
}