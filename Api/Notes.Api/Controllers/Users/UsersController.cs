using InkGoose.Api.Database;
using InkGoose.Api.Services;
using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InkGoose.Api.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DatabaseContext _context;
        private readonly AuthService _authService;
        private readonly PasswordHasher<User> _hasher;

        public UsersController(ILogger<UsersController> logger, DatabaseContext context, AuthService authService)
        {
            _logger = logger;
            _context = context;
            _authService = authService;
            _hasher = new PasswordHasher<User>();
        }

        [HttpGet(Name = "GetUser")]
        [Authorize]
        public User? GetUser()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            return user;
        }
        [HttpPost(Name = "AddUser")]
        public void AddUser(string userName, string email, string password)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                UserName = userName,
                Email = email
            };
            newUser.Password = _hasher.HashPassword(newUser, password);
            Database.Helpers.CreateUser(newUser, _context);
        }

        [HttpDelete(Name = "DeleteUser")]
        public string DeleteUser()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return "Unauthorized";
            }
            Database.Helpers.DeleteUser(user.Id, _context);
            return "Success";
        }

        [HttpPatch(Name = "UpdateUser")]
        public string UpdateUser(string? userName, string? email, string? password)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return "Unauthorized";
            }
            if (!string.IsNullOrEmpty(userName))
            {
                user.UserName = userName;
            }
            if (!string.IsNullOrEmpty(email))
            {
                user.Email = email;
            }

            if (!string.IsNullOrEmpty(password))
            {
                user.Password = _hasher.HashPassword(user, password);
            }
            user.DateModified = DateTime.UtcNow;
            Database.Helpers.UpdateUser(user, _context);
            return "Success";
        }
        [HttpPost(Name = "Authenticate")]
        public string Authenticate(string email, string password)
        {
            User? user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return "User not found";
            }
            PasswordVerificationResult passCheck = _hasher.VerifyHashedPassword(user, user.Password, password);
            if (passCheck == PasswordVerificationResult.Success)
            {
                return _authService.GenerateToken(user);
            }
            else
            {
                return "Incorrect password";
            }            
        }
        [HttpGet(Name = "SignIn")]
        [Authorize]
        public string SignIn()
        {
            return $"User {AuthService.GetEmail(HttpContext.User)} authenticated Successfully!";
        }
    }
}
