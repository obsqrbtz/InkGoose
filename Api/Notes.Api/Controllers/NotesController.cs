using InkGoose.Domain.Entities;
using InkGoose.Api.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkGoose.Api.Controllers.Notes
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly DatabaseContext _context;

        public NotesController(ILogger<NotesController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetNotesList")]
        public IEnumerable<Note> GetNotesList()
        {
            return Database.Helpers.GetNotes(_context);
        }

        [HttpGet(Name = "GetLast")]
        public Note GetLast()
        {
            return Database.Helpers.GetLast(_context);
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
            Database.Helpers.CreateNote(newNote, _context);
        }

        [HttpDelete(Name = "DeleteNote")]
        public void DeleteNote(Guid id)
        {
            Database.Helpers.DeleteNote(id, _context);
        }

        [HttpPatch(Name = "UpdateNote")]
        public void UpdateNote(Guid id, bool archived, string? title = null, string? content = null)
        {
            var note = Database.Helpers.GetNote(id, _context);
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
            Database.Helpers.UpdateNote(note, _context);
        }
    }

}
