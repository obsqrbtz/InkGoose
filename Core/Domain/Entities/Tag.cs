using InkGoose.Domain.Common;

namespace InkGoose.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string? Value { get; set; }
        public Guid UserID { get; set; }
        public Tag()
        {
            Value = string.Empty;
        }
    }
}
