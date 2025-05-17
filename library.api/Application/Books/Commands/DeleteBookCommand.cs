using library.api.Models;

namespace library.api.Application.Books.Commands
{
    public class DeleteBookCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
