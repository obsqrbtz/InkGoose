using InkGoose.Api.Notes.Controllers;
using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace InkGoose.Api.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "AddUser")]
        public void AddUser(string firstName, string lastName, string login, string email)
        {
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                FirstName = firstName,
                LastName = lastName,
                Login = login,
                Email = email,
            };
            Database.Users.DbActions.CreateUser(newUser);
        }

        [HttpDelete(Name = "DeleteUser")]
        public void DeleteUser(Guid id)
        {
            Database.Users.DbActions.DeleteUser(id);
        }

        [HttpPatch(Name = "UpdateUser")]
        public void UpdateUser(Guid id, string? firstName, string? lastName, string? login, string? email)
        {
            var user = Database.Users.DbActions.GetUser(id);
            if (!string.IsNullOrEmpty(firstName))
            {
                user.FirstName = firstName;
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                user.LastName = lastName;
            }
            if (!string.IsNullOrEmpty(login))
            {
                user.Login = login;
            }
            if (!string.IsNullOrEmpty(email))
            {
                user.Email = email;
            }
            user.DateModified = DateTime.UtcNow;
            Database.Users.DbActions.UpdateUser(user);
        }
    }
}
