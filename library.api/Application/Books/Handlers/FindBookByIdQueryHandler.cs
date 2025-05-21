using AutoMapper;
using library.api.Application.Books.Queries;
using library.api.Application.Repositories;
using library.api.Application.Services.Files;
using library.api.Models;
using MediatR;

namespace library.api.Application.Books.Handlers
{
    public class FindBookByIdQueryHandler 
        : IRequestHandler<FindBookByIdQuery, BookModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileRepositoryService _fileRepositoryService;
        private readonly IMapper _mapper;

        public FindBookByIdQueryHandler(
            IBookRepository bookRepository,
            IFileRepositoryService fileRepositoryService,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _fileRepositoryService = fileRepositoryService;
            _mapper = mapper;
        }

        public async Task<BookModel> Handle(
            FindBookByIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _bookRepository.GetById(request.Id);

            var url = await _fileRepositoryService.GetDocumentUrl(result.UrlFile);
            var model = _mapper.Map<BookModel>(result);
            model.UrlDocument = url;
            return model;
        }
    }
}
