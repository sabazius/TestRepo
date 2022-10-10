using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record AddBookCommand(Book book) : IRequest<bool>
    {
    }
}
