using System.Diagnostics.CodeAnalysis;
using InkGoose.Api.Database;
using InkGoose.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static InkGoose.Api.Controllers.Notes.NotesController;

namespace InkGoose.Api.Controllers.Tags
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TagsController(ILogger<TagsController> logger, DatabaseContext context) : ControllerBase
    {
        private readonly ILogger<TagsController> _logger = logger;
        private readonly DatabaseContext _context = context;

        [HttpGet(Name = "GetUserTags")]
        [Authorize]
        public IEnumerable<Tag> GetUserTags()
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return [];
            }
            return _context.Tags.Where(tag => tag.UserID == user.Id);
        }

        [HttpPost(Name = "Add")]
        [Authorize]
        public string Add(string value)
        {
            User? user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return "Failed to add tag";
            }
            var tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Value = value,
                UserID = user.Id
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return "Success";
        }

        [HttpDelete(Name = "Delete")]
        [Authorize]
        public void Delete(Guid id)
        {
            var user = Helpers.GetUser(HttpContext.User, _context);
            if (user is null)
            {
                return;
            }
            _context.Tags.Where(x => x.Id == id).ExecuteDelete();
        }

        // TODO: move request models somewhere
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public class TagRequestBody
        {
            [FromBody] public Guid id { get; set; }
            [FromBody] public string? value { get; set; }
            [FromBody] public string? userid { get; set; }
        }
    }

}
