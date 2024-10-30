using InkGoose.Domain.Common;

namespace InkGoose.Domain.Entities
{
    public class Note : BaseEntity
    {
        public bool Archived { get; set; }
        public bool Pinned { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Tag { get; set; }
        public string? Color { get; set; }
        public Guid UserID { get; set; }
        public Note()
        {
            Title = string.Empty;
            Content = string.Empty;
            Tag = string.Empty;
            Color = string.Empty;
        }
    }
}
