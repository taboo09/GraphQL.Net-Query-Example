using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Product.API.GraphQL
{
    public class BookSchema : Schema
    {
        public BookSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<BookQuery>();
        }        
    }
}