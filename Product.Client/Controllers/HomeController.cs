using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Client.Clients;
using Product.Client.Models;

namespace Product.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookGraphClient _client;

        public HomeController(ILogger<HomeController> logger, BookGraphClient client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var books = await _client.GetBooksAsync();

            return View(books);
        }

        public async Task<IActionResult> GetBook(string id)
        {
            // var bookId = new Guid("3A414C43-CD25-DC91-0922-D1A081809FCB");
            var bookId = new Guid(id);
            var book = await _client.GetBookAsync(bookId);

            return View("Book", book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
