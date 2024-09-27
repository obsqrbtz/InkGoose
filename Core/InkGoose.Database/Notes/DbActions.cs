using InkGoose.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InkGoose.Database.Notes
{
    public static class DbActions
    {
        public static void CreateNote(Note note)
        {
            using (DatabaseContext db = new())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }
        }
        public static IEnumerable<Note> GetNotes()
        {
            List<Note> notes = new();
            using (DatabaseContext db = new())
            {
                notes = db.Notes.ToList<Note>();
            }
            return notes;
        }
        public static Note GetLast()
        {
            Note? note;
            using (DatabaseContext db = new())
            {
                note = db.Notes.ToList().MaxBy(x => x.DateCreated);
            }
            if (note is null)
            {
                return new Note();
            }
            return note;
        }

        public static void DeleteNote(Guid id) 
        {
            using (DatabaseContext db = new())
            {
                db.Notes.Where(x => x.Id == id).ExecuteDelete();
            }
        }
        public static void UpdateNote(Note note)
        {
            using (DatabaseContext db = new())
            {
                db.Notes.Update(note);
                db.SaveChanges();
            }
        }
        public static Note GetNote(Guid id)
        {
            Note? note;
            using (DatabaseContext db = new())
            {
                note = db.Find<Note>(id);
            }
            if (note is null)
            {
                return new Note();
            }
            return note;
        }
    }

}
