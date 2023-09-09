using Microsoft.AspNetCore.Mvc;
using WarfaceLineups.Utils;
using WarfaceLineupsV1._1.Database.Models;
using WarfaceLineupsV1._1.Database.Requests;
using WarfaceLineupsV1._1.Filters;
using WarfaceLineupsV1._1.Models;
using WfTracker.Utils;

namespace WarfaceLineupsV1._1.Controllers;

public class AccountsControllers : Controller
{
    [HttpPost("api/registration")]
    public async Task<IResult> OnRegistration([FromBody]RegistrationData registrationData)
    {
        if (HandlerAccounts.IsEmailExist(registrationData.Email)) return Results.BadRequest("Аккаунт с таким email уже существует");
        if (HandlerAccounts.IsLoginExist(registrationData.Login)) return Results.BadRequest("Аккаунт с таким логином уже существует");
        if (registrationData.Password.Length < 8) return Results.BadRequest("Пароль не должен быть меньше 8 символов");
        
        var account = HandlerAccounts.Register(registrationData.Login, registrationData.Email, registrationData.Password);
        return Results.Json(new
        {
            login = account.Login,
            jwtToken = AuthService.GenerateJwtToken(account),
            accountId = account.Id,
            role = account.Role,
            isVerifiedAccount = account.IsVerifiedAccount,
            isPremiumAccount = account.IsPremiumAccount
        });
    }
    [HttpPost("api/authorization")]
    public async Task<IResult> OnAuthorization([FromBody]AuthorizationData authorizationData)
    {
        if (HandlerAccounts.IsEmailExist(authorizationData.Email) && HandlerAccounts.IsPasswordValid(authorizationData.Email, authorizationData.Password))
        {
            var account = HandlerAccounts.GetAccountByEmail(authorizationData.Email);
            return Results.Json(new
            {
                login = account.Login,
                jwtToken = AuthService.GenerateJwtToken(account),
                accountId = account.Id,
                role = account.Role,
                isVerifiedAccount = account.IsVerifiedAccount,
                isPremiumAccount = account.IsPremiumAccount
            });
        }

        return Results.BadRequest();
    }

    [HttpPost("api/authorizationbyjwt")]
    public async Task<IResult> OnAuthorizationByJwt([FromBody]AuthorizationByJwtData authorizationByJwtData)
    {
        var account = HandlerAccounts.GetAccountByLogin(authorizationByJwtData.Login);
        if (AuthService.CheckIsValidToken(authorizationByJwtData.JwtToken, authorizationByJwtData.Login))
        {
            return Results.Ok(new
            {
                login = account.Login,
                jwtToken = AuthService.GenerateJwtToken(account),
                accountId = account.Id,
                role = account.Role,
                isVerifiedAccount = account.IsVerifiedAccount,
                isPremiumAccount = account.IsPremiumAccount
            });
        }
        return Results.Unauthorized();
    }

    [AuthorizeByJwt]
    [HttpGet("api/getverificationcode")]
    public async Task<IResult>  GetVerificationCode()
    {
        var login = Request.Headers["login"];
        var account = HandlerAccounts.GetAccountByLogin(login);
        if (account.VerificationCode != "")
        {
            return Results.BadRequest("Код подтверждения уже отправлен");
        }
        if (EmailService.SendEmailAsync(account.Email, "Код подтверждения", HandlerAccounts.GenerateVerificationCodeForAccount(account)))
        {
            return Results.Ok($"Код подтверждения отправлен на почту {account.Email}");
        }
        return Results.BadRequest("Код подтверждения не был отправлен по неизвестной ошибке");
    }
    
    [AuthorizeByJwt]
    [HttpPost("api/uploadverificationcode")]
    public async Task<IResult> UploadVerificationCode([FromBody]string verificationCode)
    {
        var login = Request.Headers["login"];
        var account = HandlerAccounts.GetAccountByLogin(login);
        return HandlerAccounts.CheckIsValidVerificationCodeForAccount(account, verificationCode) ? Results.Ok() : Results.BadRequest();
    }
}