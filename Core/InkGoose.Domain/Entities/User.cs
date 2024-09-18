using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGoose.Domain.Entities
{
    public sealed class User
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User()
        {
            Login = string.Empty; 
            FirstName = string.Empty; 
            LastName = string.Empty;
        }
    }
}
