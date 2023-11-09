using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserRegistration.WebApi.Data;
using UserRegistration.WebApi.Util;
using UserRegistration.WebApi.ViewModels;

namespace UserRegistration.WebApi.EndPoints
{
    public static class LoginEndPoints
    {
        public static void MapLoginEndpoints(this IEndpointRouteBuilder routes, byte[] key)
        {
            routes.MapPost("/api/Login", async (LoginVM login, UserRegistrationContext db) =>
            {
                var findUser = await db.Users.FirstOrDefaultAsync(u => u.Login.Equals(login.Login));
                if (findUser is null)
                {
                    return Results.NotFound();
                }

                if (PasswordService.ComparePassword(login.Password, findUser.Password))
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, findUser.Name ?? string.Empty),
                        new Claim(ClaimTypes.GivenName, findUser.Login),
                        new Claim(ClaimTypes.NameIdentifier, findUser.Id.ToString()),
                    };

                    JwtSecurityToken token = new(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

                    string tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

                    return Results.Ok(tokenResult);
                }

                return Results.Unauthorized();
            })
            .WithName("Login")
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
