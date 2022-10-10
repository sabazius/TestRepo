using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record DeleteBookCommand(int bookId) : IRequest<bool>
    {
    }
}
