namespace library.api.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Authors { get; set; }
        public string Isdn { get; set; }
        public string UrlFile { get; set; }
    }
}
