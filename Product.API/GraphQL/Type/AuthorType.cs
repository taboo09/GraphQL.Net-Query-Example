using GraphQL.Types;
using Product.API.Entities;

namespace Product.API.GraphQL.Type
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Field(t => t.Id);
            Field(t => t.FirstName);
            Field(t => t.LastName);
        }
    }
}