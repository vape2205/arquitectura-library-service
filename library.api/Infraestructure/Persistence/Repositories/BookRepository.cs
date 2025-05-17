using library.api.Application.Repositories;
using library.api.Entities;
using library.api.Infraestructure.Persistence.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace library.api.Infraestructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private const string _booksCollectionName = "Books";

        private readonly IOptions<MongoDbSettings> _settings;

        public BookRepository(IOptions<MongoDbSettings> settings)
        {
            _settings = settings;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        public async Task<IEnumerable<Book>> GetAll(int pageNumber, int pageSize)
        {
            var bookCollection = GetBookCollection();

            var queryable = bookCollection.AsQueryable();

            var list = await queryable
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            return list;
        }

        public async Task<Book> GetByIsdn(string isdn)
        {
            var bookCollection = GetBookCollection();

            var filter = new FilterDefinitionBuilder<Book>().Where(u => u.Isdn == isdn);

            var result = (await bookCollection.FindAsync(filter)).SingleOrDefault();

            return result;
        }

        public async Task<Book> GetById(Guid id)
        {
            var bookCollection = GetBookCollection();

            var filter = new FilterDefinitionBuilder<Book>().Where(u => u.Id == id);

            var result = (await bookCollection.FindAsync(filter)).SingleOrDefault();

            return result;
        }

        public async Task<Book> Insert(Book book)
        {
            var bookCollection = GetBookCollection();

            var filter = new FilterDefinitionBuilder<Book>().Where(u => u.Id == book.Id);

            await bookCollection.InsertOneAsync(book);

            return book;
        }

        public async Task Update(Book book)
        {
            var bookCollection = GetBookCollection();

            var filter = new FilterDefinitionBuilder<Book>().Where(u => u.Id == book.Id);

            var result = await bookCollection.ReplaceOneAsync(filter, book);

            if (!result.IsAcknowledged)
                throw new Exception("Book could not be updated. Result is not acknowledged");
        }

        public async Task Delete(Guid id)
        {
            var bookCollection = GetBookCollection();

            var filter = new FilterDefinitionBuilder<Book>().Where(u => u.Id == id);

            var result = await bookCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Book>> FindByTitle(string queryString, int pageNumber, int pageSize)
        {
            var bookCollection = GetBookCollection();
            var queryable = bookCollection.AsQueryable();

            var list = await queryable
                            .Where(x => x.Title.Contains(queryString))
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Book>> FindByAuthor(string queryString, int pageNumber, int pageSize)
        {
            var bookCollection = GetBookCollection();
            var queryable = bookCollection.AsQueryable();

            var list = await queryable
                        .Where(x => x.Authors.Contains(queryString))
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            return list;
        }

        private MongoClient GetClient()
        {
            return new MongoClient(_settings.Value.ConnectionString);
        }

        private IMongoCollection<Book> GetBookCollection()
        {
            var client = GetClient();
            var database = client.GetDatabase(_settings.Value.DatabaseName);
            var collection = database.GetCollection<Book>(_booksCollectionName);
            return collection;
        }
    }
}
