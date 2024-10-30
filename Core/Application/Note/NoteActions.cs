namespace InkGoose.Application.Note
{
    public static class NoteActions
    {
        public static Domain.Entities.Note CreateNote(string title, string content)
        {
            return new Domain.Entities.Note
            {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Archived = false,
                Title = title,
                Pinned = false,
                Content = content
            };
        }
    }
}
