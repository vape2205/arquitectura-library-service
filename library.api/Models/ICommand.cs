using FluentResults;
using MediatR;

namespace library.api.Models
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
    }

    public interface ICommand : IRequest<Result>
    {
    }
}
