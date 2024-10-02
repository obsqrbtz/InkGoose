using InkGoose.Domain.Entities;
using InkGoose.Api.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InkGoose.Api.Services;

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
        [Authorize]
        public IEnumerable<Note> GetNotesList()
        {
            var user = InkGoose.Api.Controllers.Helpers.GetUser(HttpContext.User, _context);
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
            var user = InkGoose.Api.Controllers.Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return null;
            }
            return Database.Helpers.GetLast(_context, user.Id);
        }

        [HttpPost(Name = "AddNote")]
        [Authorize]
        public void AddNote([FromBody] RequestBody requestParams)
        {
            var user = Api.Controllers.Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            Note newNote = new Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Archived = false,
                Title = requestParams.title,
                Content = requestParams.content,
                UserID = user.Id
            };
            Database.Helpers.CreateNote(newNote, _context);
        }

        [HttpDelete(Name = "DeleteNote")]
        [Authorize]
        public void DeleteNote(Guid id)
        {
            var user = Api.Controllers.Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            Database.Helpers.DeleteNote(id, _context, user.Id);
        }
        // TODO: move request models somewhere
        public class RequestBody
        {
            [FromBody] public Guid id { get; set; }
            [FromBody] public bool archived { get; set; }
            [FromBody] public string? title { get; set; }
            [FromBody] public string? content { get; set; }
        }

        [HttpPatch(Name = "UpdateNote")]
        [Authorize]
        public void UpdateNote([FromBody]RequestBody requestParams)
        {
            var user = Api.Controllers.Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            var note = Database.Helpers.GetNote(requestParams.id, _context);
            if (note.UserID != user.Id)
            {
                return;
            }
            note.Archived = requestParams.archived;
            if (!string.IsNullOrEmpty(requestParams.title))
            {
                note.Title = requestParams.title;
            }
            if (!string.IsNullOrEmpty(requestParams.content))
            {
                note.Content = requestParams.content;
            }
            note.DateModified = DateTime.UtcNow;
            Database.Helpers.UpdateNote(note, _context);
        }
    }

}
