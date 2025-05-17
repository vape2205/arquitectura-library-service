using AutoMapper;
using library.api.Application.Books.Queries;
using library.api.Application.Repositories;
using library.api.Models;
using MediatR;

namespace library.api.Application.Books.Handlers
{
    public class FindBooksByAuthorQueryHandler
        : IRequestHandler<FindBooksByAuthorQuery, IEnumerable<BookModel>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public FindBooksByAuthorQueryHandler(
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookModel>> Handle(
            FindBooksByAuthorQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await _bookRepository.FindByAuthor(request.QueryString, 
                request.PageNumber, request.PageSize);
            return _mapper.Map<IEnumerable<BookModel>>(result);
        }
    }
}
