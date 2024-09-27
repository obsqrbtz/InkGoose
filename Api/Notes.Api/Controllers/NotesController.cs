using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InkGoose.Api.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetNotesList")]
        public IEnumerable<Note> GetNotesList()
        {
            return Database.Notes.DbActions.GetNotes();
        }

        [HttpGet(Name = "GetLast")]
        public Note GetLast()
        {
            return Database.Notes.DbActions.GetLast();
        }

        [HttpPost(Name = "AddNote")]
        public void AddNote(string title, string content)
        {
            Note newNote = new Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Archived = false,
                Title = title,
                Content = content
            };
            Database.Notes.DbActions.CreateNote(newNote);
        }

        [HttpDelete(Name = "DeleteNote")]
        public void DeleteNote(Guid id)
        {
            Database.Notes.DbActions.DeleteNote(id);
        }

        [HttpPatch(Name = "UpdateNote")]
        public void UpdateNote(Guid id, bool archived, string? title = null, string? content = null)
        {
            var note = Database.Notes.DbActions.GetNote(id);
            note.Archived = archived;
            if (!string.IsNullOrEmpty(title))
            {
                note.Title = title;
            }
            if (!string.IsNullOrEmpty(content))
            {
                note.Content = content;
            }
            note.DateModified = DateTime.UtcNow;
            Database.Notes.DbActions.UpdateNote(note);
        }
    }

}
