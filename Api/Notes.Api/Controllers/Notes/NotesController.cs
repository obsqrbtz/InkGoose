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
        public IEnumerable<Tag> GetTags(Guid noteId)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return [];
            }
            var note = Database.Helpers.GetNote(noteId, _context);
            // TODO: move to helpers
            var userTags = _context.Tags.Where(x => x.UserID == user.Id).ToList();
            var tags = new List<Tag>();
            foreach(Guid tagId in note.Tags)
            {
                var tag = userTags.Find(x => x.Id == tagId);
                if (tag is not null)
                {
                    tags.Add(tag);
                }
            }
            return tags;
        }

        [HttpPatch(Name = "AddTag")]
        [Authorize]
        public string AddTag(Guid noteId, string value)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return "Failed to add tag";
            }
            var note = Database.Helpers.GetNote(noteId, _context);
            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Value = value,
                UserID = user.Id
            };
            _context.Tags.Add(tag);
            note.Tags.Add(tag.Id);
            _context.SaveChanges();
            Database.Helpers.UpdateNote(note, _context);
            return "Success";
        }

        [HttpDelete(Name = "DeleteTag")]
        [Authorize]
        public string DeleteTag(Guid noteId, Guid tagId)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return "Failed to add tag";
            }
            var note = Database.Helpers.GetNote(noteId, _context);
            note.Tags.Remove(tagId);
            Database.Helpers.UpdateNote(note, _context);
            return "Success";
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
            if (!string.IsNullOrEmpty(noteRequestParams.color))
            {
                note.Color = noteRequestParams.color;
            }
            note.DateModified = DateTime.UtcNow;
            Database.Helpers.UpdateNote(note, _context);
        }
    }

}
