using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InkGoose.Api.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private static List<Note> NotesPlaceholder =
            [
                new() {Id = Guid.NewGuid(), DateCreated = DateTime.Now, DateModified = DateTime.Now, Archived = false, Title = "First note",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                new() {Id = Guid.NewGuid(), DateCreated = DateTime.Now, DateModified = DateTime.Now, Archived = false, Title = "Second note",
                    Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
                new() {Id = Guid.NewGuid(), DateCreated = DateTime.Now, DateModified = DateTime.Now, Archived = true, Title = "Third note",
                    Content = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."},
                new() {Id = Guid.NewGuid(), DateCreated = DateTime.Now, DateModified = DateTime.Now, Archived = false, Title = "Fourth note",
                    Content = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\r\n\r\n"}
            ];

        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetNotesList")]
        public IEnumerable<Note> GetNotesList()
        {
            return NotesPlaceholder;
        }

        [HttpGet(Name = "GetLast")]
        public Note GetLast()
        {
            return NotesPlaceholder.MaxBy(x => x.DateCreated);
        }

        [HttpPost(Name = "AddNote")]
        public void AddNote(string title, string content)
        {
            NotesPlaceholder.Add(new Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Archived = false,
                Title = title,
                Content = content
            });
        }

        [HttpDelete(Name = "DeleteNote")]
        public void DeleteNote(Guid id)
        {
            var note = NotesPlaceholder.First(x => x.Id == id);
            NotesPlaceholder.Remove(note);
        }

        [HttpPatch(Name = "UpdateNote")]
        public void UpdateNote(Guid id, bool archived, string? title = null, string? content = null)
        {
            var noteIdx = NotesPlaceholder.FindIndex(x => x.Id == id);
            NotesPlaceholder[noteIdx].Archived = archived;
            if (!string.IsNullOrEmpty(title))
            {
                NotesPlaceholder[noteIdx].Title = title;
            }
            if (!string.IsNullOrEmpty(content))
            {
                NotesPlaceholder[noteIdx].Content = content;
            }
            NotesPlaceholder[noteIdx].DateModified = DateTime.Now;
        }
    }

}
