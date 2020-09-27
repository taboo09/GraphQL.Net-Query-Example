using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Product.API.Entities;

namespace Books.API.Repository
{
    public interface IBookRepository
    {
        Task<Book> GetBookAsync(Guid id);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> bookIds);
        void AddBook(Book book);
        Task<bool> SaveChangesAsync();
    }
}