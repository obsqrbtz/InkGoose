using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGoose.Application.User
{
    public static class UserActions
    {
        public static Domain.Entities.User CreateUser(string firstName, string lastName, string login, string email)
        {
            return new Domain.Entities.User()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                FirstName = firstName,
                LastName = lastName,
                Login = login,
                Email = email
            };
        }
    }
}
