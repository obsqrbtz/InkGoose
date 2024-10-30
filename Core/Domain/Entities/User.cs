using InkGoose.Domain.Common;

namespace InkGoose.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            UserName = string.Empty; 
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
