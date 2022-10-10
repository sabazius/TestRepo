using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAll();

            if (!books.Contains(request.book))
            {
                return await _bookRepository.Add(request.book);
            }

            return false;
        }
    }
}
