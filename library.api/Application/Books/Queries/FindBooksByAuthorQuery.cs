using library.api.Models;

namespace library.api.Application.Books.Queries
{
    public class FindBooksByAuthorQuery : IQuery<IEnumerable<BookModel>>
    {
        public string QueryString { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
