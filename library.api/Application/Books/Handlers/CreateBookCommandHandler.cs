using AutoMapper;
using FluentResults;
using library.api.Application.Books.Commands;
using library.api.Application.Repositories;
using library.api.Application.Services.Files;
using library.api.Entities;
using MediatR;

namespace library.api.Application.Books.Handlers
{
    public class CreateBookCommandHandler
        : IRequestHandler<CreateBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileRepositoryService _fileRepositoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookCommandHandler> _logger;

        public CreateBookCommandHandler(
            IBookRepository bookRepository,
            IFileRepositoryService fileRepositoryService,
            IMapper mapper,
            ILogger<CreateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _fileRepositoryService = fileRepositoryService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.GetByIsdn(request.Isdn);
            if (existingBook != null)
            {
                return Result.Fail("El libro ya existe");
            }

            var book = _mapper.Map<Book>(request);

            var prefix = "books/";
            var extension = ".pdf";
            string fileName = $"{prefix}{book.Id}{extension}";
            string contentType = "application/pdf";
            byte[] bytesContent = Convert.FromBase64String(request.FileBase64);

            try
            {
                await _fileRepositoryService.Upload(fileName, contentType, bytesContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
            }

            book.UrlFile = fileName;
            await _bookRepository.Insert(book);
            return Result.Ok().WithSuccess(book.Id.ToString());
        }
    }
}
