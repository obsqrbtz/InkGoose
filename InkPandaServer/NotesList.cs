namespace InkPandaServer
{
    public class NotesList
    {
        public List<Note> Notes { get; set; }
        public NotesList()
        {
            Notes = new();
        }
    }

    public class Note
    {
        public uint Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Archived { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Note()
        {
            Title = string.Empty;
            Content = string.Empty;
        }
    }
}
