using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using Product.Client.Models;

namespace Product.Client.Clients
{
    public class BookGraphClient
    {
        private readonly IGraphQLClient _client;
        public BookGraphClient(IGraphQLClient  client)
        {
            _client = client;
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    query GetBook($bookId: ID!) {
                        book(id: $bookId) {
                            id
                            title
                            description
                            author {
                                firstName lastName
                            }
                        }
                    }",
                OperationName = "GetBook",
                Variables = new { bookId = id }
            };

            var response = await _client.SendQueryAsync<ResponseBook>(query);

            var book = response.Data.Book;

            return book;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    query GetBooks {
                        books {
                            id
                            title
                            description
                            author {
                                firstName
                                lastName
                            }
                        }
                    }"
            };

            var response = await _client.SendQueryAsync<ListOfBooks>(query);

            var books = response.Data.Books;

            return books;
        }
    }
}