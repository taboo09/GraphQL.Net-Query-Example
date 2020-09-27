using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.FakeData;

namespace Product.API.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options) :  base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var data = Data.GenerateFakeData(10);

            modelBuilder.Entity<Author>().HasData(data.AuthorsFake);
            modelBuilder.Entity<Book>().HasData(data.BooksFake);
        }        
    }
}