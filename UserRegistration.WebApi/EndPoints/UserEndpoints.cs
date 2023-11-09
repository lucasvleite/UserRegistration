using Microsoft.EntityFrameworkCore;
using UserRegistration.WebApi.Data;
using UserRegistration.WebApi.Models;
using UserRegistration.WebApi.Util;

namespace UserRegistration.WebApi.EndPoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/User", async (UserRegistrationContext db) => await db.Users.ToListAsync())
            .WithName("GetAllUsers")
            // .RequireAuthorization()
            .Produces<List<User>>(StatusCodes.Status200OK);

            routes.MapGet("/api/User/{id}", async (string id, UserRegistrationContext db) =>
            {
                return await db.Users.FindAsync(Guid.Parse(id))
                    is User model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetUserById")
            // .RequireAuthorization()
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPut("/api/User/{id}", async (Guid id, User user, UserRegistrationContext db) =>
            {
                var result = await db.Users.FindAsync(id);

                if (result is null)
                {
                    return Results.NotFound();
                }

                if (user.Password != string.Empty)
                {
                    result.Password = user.Password.Encrypt();
                }

                result.Login = user.Login;
                result.Name = user.Name;

                db.Update(result);
                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateUser")
            // .RequireAuthorization()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

            routes.MapPost("/api/User/", async (User user, UserRegistrationContext db) =>
            {
                user.Password = user.Password.Encrypt();
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Created($"/Users/{user.Id}", user);
            })
            .WithName("CreateUser")
            // .RequireAuthorization()
            .Produces<User>(StatusCodes.Status201Created);

            routes.MapDelete("/api/User/{id}", async (Guid id, UserRegistrationContext db) =>
            {
                if (await db.Users.FindAsync(id) is User user)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return Results.Ok(user);
                }

                return Results.NotFound();
            })
            .WithName("DeleteUser")
            // .RequireAuthorization()
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}