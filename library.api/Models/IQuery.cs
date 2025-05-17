using MediatR;

namespace library.api.Models
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
