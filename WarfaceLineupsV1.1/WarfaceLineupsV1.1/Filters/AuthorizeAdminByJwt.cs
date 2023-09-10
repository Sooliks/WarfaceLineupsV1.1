using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WarfaceLineupsV1._1.Database.Requests;
using WfTracker.Utils;

namespace WarfaceLineupsV1._1.Filters;

public class AuthorizeAdminByJwt : Attribute, IResourceFilter
{
    private readonly string[] _headers;
    public AuthorizeAdminByJwt(params string[] headers)
    {
        _headers = headers;
    }
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var jwtToken = context.HttpContext.Request.Headers["authorization"];
        var login = context.HttpContext.Request.Headers["login"];
        
        if (!AuthService.CheckIsValidToken(jwtToken, login))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var account = HandlerAccounts.GetAccountByLogin(login);
        if (account.Role != "admin")
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}