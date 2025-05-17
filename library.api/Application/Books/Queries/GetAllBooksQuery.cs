using library.api.Models;

namespace library.api.Application.Books.Queries
{
    public class GetAllBooksQuery : IQuery<IEnumerable<BookModel>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
