﻿using BookStore.BL.Interfaces;
using BookStore.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookService.Delete(request.bookId);
        }
    }
}
