using library.api.Models;

namespace library.api.Application.Books.Queries
{
    public class FindBookByIdQuery : IQuery<BookModel>
    {
        public Guid Id { get; set; }
    }
}
