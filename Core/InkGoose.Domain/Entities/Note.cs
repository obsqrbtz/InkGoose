using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InkGoose.Domain.Common;

namespace InkGoose.Domain.Entities
{
    public class Note : BaseEntity
    {
        public bool Archived { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserID { get; set; }
        public Note()
        {
            Title = string.Empty;
            Content = string.Empty;
        }
    }
}
