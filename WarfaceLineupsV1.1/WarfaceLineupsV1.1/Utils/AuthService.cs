using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WarfaceLineupsV1._1.Database.Models;


namespace WfTracker.Utils;

public class AuthService
{
    private static readonly string SecretString = "3y4O54yGfDg43fFffhG3#ff.,,,,```'94'.jfhwEfg";
    private static readonly string Issuer = "AmogusSus";
    private static readonly string Audience = "MyAuthClient";
    private static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretString));
    
    public static string GenerateJwtToken(Account account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Login)
        };
        var jwtToken = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return encodedJwtToken;
    }
    public static bool CheckIsValidToken(string token, string claimLogin)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateAudience = false,
                ValidateIssuer = false
            };
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var login = jwtToken.Claims.First(x => x.Value == claimLogin).Value;
            if (login != null) return true;
        }
        catch (Exception e)
        {
            return false;
        }
        return false;
    }
    public static string GenerateVerificationCode(int min = 100000, int max = 999999)
    {
        Random random = new Random();
        return random.Next(min, max).ToString();
    }
    
}