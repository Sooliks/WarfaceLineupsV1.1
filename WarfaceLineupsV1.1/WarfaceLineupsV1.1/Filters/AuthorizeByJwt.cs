using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WfTracker.Utils;

namespace WarfaceLineupsV1._1.Filters;

public class AuthorizeByJwt : Attribute, IResourceFilter
{
    private readonly string[] _headers;
    public AuthorizeByJwt(params string[] headers)
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
    }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}