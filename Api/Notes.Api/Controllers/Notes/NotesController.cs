using System.Diagnostics.CodeAnalysis;
using InkGoose.Api.Database;
using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InkGoose.Api.Controllers.Notes
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController(ILogger<NotesController> logger, DatabaseContext context) : ControllerBase
    {
        private readonly ILogger<NotesController> _logger = logger;
        private readonly DatabaseContext _context = context;

        [HttpGet(Name = "GetNotesList")]
        [Authorize]
        public IEnumerable<Note> GetNotesList()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return [];
            }
            return Database.Helpers.GetNotes(_context, user.Id);
        }

        [HttpGet(Name = "GetLast")]
        [Authorize]
        public Note? GetLast()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return null;
            }
            return Database.Helpers.GetLast(_context, user.Id);
        }
        
        [HttpGet(Name = "GetTags")]
        [Authorize]
        public IEnumerable<string?> GetTags()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return [];
            }
            var notes = Database.Helpers.GetNotes(_context, user.Id);
            var tags = notes.Where(x => x.UserID == user.Id).Select(y => y.Tag).Distinct();
            return tags;
        }

        [HttpPost(Name = "AddNote")]
        [Authorize]
        public void AddNote([FromBody] NoteRequestBody noteRequestParams)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            var newNote = new Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Archived = false,
                Tag = noteRequestParams.tag,
                Pinned = noteRequestParams.pinned,
                Color = noteRequestParams.color,
                Title = noteRequestParams.title,
                Content = noteRequestParams.content,
                UserID = user.Id
            };
            Database.Helpers.CreateNote(newNote, _context);
        }

        [HttpDelete(Name = "DeleteNote")]
        [Authorize]
        public void DeleteNote(Guid id)
        {
            var user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            Database.Helpers.DeleteNote(id, _context, user.Id);
        }
        // TODO: move request models somewhere
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public class NoteRequestBody
        {
            [FromBody] public Guid id { get; set; }
            [FromBody] public bool archived { get; set; }
            [FromBody] public bool pinned { get; set; }
            [FromBody] public string? title { get; set; }
            [FromBody] public string? content { get; set; }
            [FromBody] public string? tag { get; set; }
            [FromBody] public string? color { get; set; }
        }

        [HttpPatch(Name = "UpdateNote")]
        [Authorize]
        public void UpdateNote([FromBody]NoteRequestBody noteRequestParams)
        {
            var user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            var note = Database.Helpers.GetNote(noteRequestParams.id, _context);
            if (note.UserID != user.Id)
            {
                return;
            }
            note.Archived = noteRequestParams.archived;
            note.Pinned = noteRequestParams.pinned;
            if (!string.IsNullOrEmpty(noteRequestParams.title))
            {
                note.Title = noteRequestParams.title;
            }
            if (!string.IsNullOrEmpty(noteRequestParams.content))
            {
                note.Content = noteRequestParams.content;
            }
            if (!string.IsNullOrEmpty(noteRequestParams.tag))
            {
                note.Tag = noteRequestParams.tag;
            }
            if (!string.IsNullOrEmpty(noteRequestParams.color))
            {
                note.Color = noteRequestParams.color;
            }
            note.DateModified = DateTime.UtcNow;
            Database.Helpers.UpdateNote(note, _context);
        }
    }

}
