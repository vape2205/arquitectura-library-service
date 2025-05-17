using AutoMapper;
using library.api.Application.Books.Queries;
using library.api.Application.Repositories;
using library.api.Models;
using MediatR;

namespace library.api.Application.Books.Handlers
{
    public class FindBookByIdQueryHandler 
        : IRequestHandler<FindBookByIdQuery, BookModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public FindBookByIdQueryHandler(
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookModel> Handle(
            FindBookByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _bookRepository.GetById(request.Id);
            return _mapper.Map<BookModel>(result);
        }
    }
}
