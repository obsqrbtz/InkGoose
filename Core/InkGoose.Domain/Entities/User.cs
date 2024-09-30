using InkGoose.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGoose.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public User()
        {
            Login = string.Empty; 
            FirstName = string.Empty; 
            LastName = string.Empty;
            Email = string.Empty;
        }
    }
}
