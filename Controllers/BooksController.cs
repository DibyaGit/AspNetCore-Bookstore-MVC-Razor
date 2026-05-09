using Microsoft.AspNetCore.Mvc;
using BookstoreApp.Repositories;
using BookstoreApp.Models;
using System.Threading.Tasks;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepository<Book> _bookRepository;

        public BooksController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // MVC View for listing books
        [Route("Books")]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        // MVC View for book details
        [Route("Books/Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
