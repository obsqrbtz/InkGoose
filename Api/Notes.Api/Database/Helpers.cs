using InkGoose.Api.Services;
using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InkGoose.Api.Database
{
    public static class Helpers
    {
        public static void CreateNote(Note note, DatabaseContext db)
        {
            db.Notes.Add(note);
            db.SaveChanges();
        }
        public static IEnumerable<Note> GetNotes(DatabaseContext db, Guid userId)
        {
            List<Note> notes = db.Notes.OrderByDescending(x => x.DateCreated).ToList<Note>().FindAll(y => y.UserID == userId);
            return notes;
        }
        public static Note GetLast(DatabaseContext db, Guid userId)
        {
            Note? note = db.Notes.ToList().FindAll(x => x.UserID == userId).MaxBy(x => x.DateCreated);
            if (note is null)
            {
                return new Note();
            }
            return note;
        }

        public static void DeleteNote(Guid id, DatabaseContext db, Guid userId)
        {
            db.Notes.Where(x => x.Id == id && x.UserID == userId).ExecuteDelete();
        }
        public static void UpdateNote(Note note, DatabaseContext db)
        {
            db.Notes.Update(note);
            db.SaveChanges();
        }
        public static Note GetNote(Guid id, DatabaseContext db)
        {
            var note = db.Find<Note>(id);
            if (note is null)
            {
                return new Note();
            }
            return note;
        }
        public static void CreateUser(User user, DatabaseContext db)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public static IEnumerable<User> GetUsers(DatabaseContext db)
        {
            List<User> users = db.Users.ToList<User>();
            return users;
        }
        public static void DeleteUser(Guid id, DatabaseContext db)
        {
            db.Users.Where(x => x.Id == id).ExecuteDelete();
        }
        public static void UpdateUser(User user, DatabaseContext db)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
        public static User GetUser(Guid id, DatabaseContext db)
        {
            var user = db.Find<User>(id);
            if (user is null)
            {
                return new User();
            }
            return user;
        }
        public static User? GetUser(string email, DatabaseContext db)
        {
            // TODO: check for default values
            User? user = db.Users.FirstOrDefault(x => x.Email == email);
            if (user is null)
            {
                return new User();
            }
            return user;
        }
    }
}
