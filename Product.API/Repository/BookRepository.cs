using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.API.Context;
using Product.API.Entities;

namespace Books.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BookContext context, ILogger<BookRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Add(book);
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _context.Books
                .Include(x => x.Author)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds)
        {
            return await _context.Books
                .Where(x => bookIds.Contains(x.Id))
                .Include(x => x.Author)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}