using FluentResults;
using library.api.Application.Books.Commands;
using library.api.Application.Repositories;
using MediatR;

namespace library.api.Application.Books.Handlers
{
    public class DeleteBookCommandHandler
        : IRequestHandler<DeleteBookCommand, Result>
    {

        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(
            IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(DeleteBookCommand request, 
            CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.GetById(request.Id);
            if (existingBook == null)
            {
                return Result.Fail("El libro no existe.");
            }

            await _bookRepository.Delete(request.Id);
            return Result.Ok();
        }
    }
}
