using System.Security.Claims;
using InkGoose.Api.Database;
using InkGoose.Api.Services;
using InkGoose.Domain.Entities;

namespace InkGoose.Api.Controllers
{
    public static class Helpers
    {
        public static User? GetUser(ClaimsPrincipal userClaims, DatabaseContext db)
        {
            string? email = AuthService.GetEmail(userClaims);
            if (email is null)
            {
                return null;
            }
            User? user = Database.Helpers.GetUser(email, db);
            return user;
        }
    }
}
