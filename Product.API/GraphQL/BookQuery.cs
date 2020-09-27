using System;
using Books.API.Repository;
using GraphQL;
using GraphQL.Types;
using Product.API.GraphQL.Type;

namespace Product.API.GraphQL
{
    public class BookQuery : ObjectGraphType
    {
        private readonly IBookRepository _bookRepository;
        public BookQuery(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

            Field<ListGraphType<BookType>>("books", resolve: context => _bookRepository.GetBooksAsync());

            Field<BookType>("book", 
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => 
                {
                    var id = context.GetArgument<Guid>("id");
                    return _bookRepository.GetBookAsync(id);
                }
            );
        }
    }
}