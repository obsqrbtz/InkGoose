using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InkGoose.Notes.Api.Database
{
    public static class Helpers
    {
        public static void CreateNote(Note note, DatabaseContext db)
        {
            db.Notes.Add(note);
            db.SaveChanges();
        }
        public static IEnumerable<Note> GetNotes(DatabaseContext db)
        {
            List<Note> notes = new();
            notes = db.Notes.ToList<Note>();
            return notes;
        }
        public static Note GetLast(DatabaseContext db)
        {
            Note? note;
            note = db.Notes.ToList().MaxBy(x => x.DateCreated);
            if (note is null)
            {
                return new Note();
            }
            return note;
        }

        public static void DeleteNote(Guid id, DatabaseContext db)
        {
            db.Notes.Where(x => x.Id == id).ExecuteDelete();
        }
        public static void UpdateNote(Note note, DatabaseContext db)
        {
            db.Notes.Update(note);
            db.SaveChanges();
        }
        public static Note GetNote(Guid id, DatabaseContext db)
        {
            Note? note;
            note = db.Find<Note>(id);
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
            List<User> users = new();
            users = db.Users.ToList<User>();
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
            User? user;
            user = db.Find<User>(id);
            if (user is null)
            {
                return new User();
            }
            return user;
        }
    }
}
