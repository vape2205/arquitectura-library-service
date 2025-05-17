using library.api.Models;

namespace library.api.Application.Books.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Authors { get; set; }
        public string Isdn { get; set; }
        public string FileBase64 { get; set; }
    }
}
