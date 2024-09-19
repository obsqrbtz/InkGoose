using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace InkGoose.Api.Notes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private string? _connectionString;
        private MongoClient _mongoClient;
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
            //_connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            _connectionString = "mongodb://localhost:27017";
            if (_connectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }
            _mongoClient = new MongoClient(_connectionString);
        }

        [HttpGet(Name = "GetNotesList")]
        public IEnumerable<Note> GetNotesList()
        {
            return NotesPlaceholder;
        }

        [HttpGet(Name = "GetLast")]
        public Note GetLast()
        {
            var result = NotesPlaceholder.MaxBy(x => x.DateCreated);
            if (result is null)
            {
                return new Note();
            }
            return result;
        }

        [HttpPost(Name = "AddNote")]
        public void AddNote(string title, string content)
        {
            var collection = _mongoClient.GetDatabase("InkGoose").GetCollection<Note>("Notes");
            Note newNote = new Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Archived = false,
                Title = title,
                Content = content
            };
            collection.InsertOne(newNote);
            //NotesPlaceholder.Add(newNote)
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
