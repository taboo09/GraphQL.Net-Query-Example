using GraphQL.Types;
using Product.API.Entities;

namespace Product.API.GraphQL.Type
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(t => t.Id);
            Field(t => t.Title).Description("Book title");
            Field(t => t.Description);
            Field<AuthorType>("author");
        }
    }
}