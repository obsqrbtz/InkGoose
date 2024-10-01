using InkGoose.Api.Notes.Controllers;
using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using InkGoose.Notes.Api.Database;

namespace InkGoose.Api.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DatabaseContext _context;

        public UsersController(ILogger<UsersController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
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
            InkGoose.Notes.Api.Database.Helpers.CreateUser(newUser, _context);
        }

        [HttpDelete(Name = "DeleteUser")]
        public void DeleteUser(Guid id)
        {
            InkGoose.Notes.Api.Database.Helpers.DeleteUser(id, _context);
        }

        [HttpPatch(Name = "UpdateUser")]
        public void UpdateUser(Guid id, string? firstName, string? lastName, string? login, string? email)
        {
            var user = InkGoose.Notes.Api.Database.Helpers.GetUser(id, _context);
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
            InkGoose.Notes.Api.Database.Helpers.UpdateUser(user, _context);
        }
    }
}
