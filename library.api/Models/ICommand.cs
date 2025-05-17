using FluentResults;
using MediatR;

namespace library.api.Models
{
    public interface ICommand : IRequest<Result>
    {
    }
}
