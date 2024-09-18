using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkGoose.Application.Note
{
    public static class NoteActions
    {
        public static Domain.Entities.Note CreateNote(string title, string content)
        {
            return new Domain.Entities.Note()
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Archived = false,
                Title = title,
                Content = content
            };
        }
    }
}
