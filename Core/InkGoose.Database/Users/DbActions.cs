using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InkGoose.Database.Users
{
    public static class DbActions
    {
        public static void CreateUser(User user)
        {
            using (DatabaseContext db = new())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static IEnumerable<User> GetUsers()
        {
            List<User> users = new();
            using (DatabaseContext db = new())
            {
                users = db.Users.ToList<User>();
            }
            return users;
        }
        public static void DeleteUser(Guid id)
        {
            using (DatabaseContext db = new())
            {
                db.Users.Where(x => x.Id == id).ExecuteDelete();
            }
        }
        public static void UpdateUser(User user)
        {
            using (DatabaseContext db = new())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
        public static User GetUser(Guid id)
        {
            User? user;
            using (DatabaseContext db = new())
            {
                user = db.Find<User>(id);
            }
            if (user is null)
            {
                return new User();
            }
            return user;
        }
    }
}
