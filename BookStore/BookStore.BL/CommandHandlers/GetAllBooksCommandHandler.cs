using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetAllBooksCommandHandler : 
        IRequestHandler<GetAllBooksCommand, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAll();
        }
    }
}
