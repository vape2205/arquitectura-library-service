using library.api.Entities;
using library.api.Models;

namespace library.api.Application.Repositories
{
    public interface IBookRepository 
    {
        Task<IEnumerable<Book>> GetAll(int pageNumber, int pageSize);
        Task<Book> GetByIsdn(string isdn);
        Task<Book> GetById(Guid id);
        Task<Book> Insert(Book obj);
        Task Update(Book obj);
        Task Delete(Guid id);
        Task<IEnumerable<Book>> FindByTitle(string queryString, int pageNumber, int pageSize);
        Task<IEnumerable<Book>> FindByAuthor(string queryString, int pageNumber, int pageSize);
    }
}
