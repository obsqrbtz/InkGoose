using System.Security.Claims;
using InkGoose.Api.Database;
using InkGoose.Api.Services;
using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InkGoose.Api.Controllers
{
    public static class Helpers
    {
        public static User? GetUser(ClaimsPrincipal userClaims, DatabaseContext db)
        {
            var email = AuthService.GetEmail(userClaims);
            if (email is null)
            {
                return null;
            }
            var user = Database.Helpers.GetUser(email, db);
            if (user is null)
            {
                return null;
            }
            return user;
        }
    }
}
