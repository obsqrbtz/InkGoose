namespace InkGoose.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        // TODO: ensure that DateTime is always in UTC (ex: DateTime.UtcNow)
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
