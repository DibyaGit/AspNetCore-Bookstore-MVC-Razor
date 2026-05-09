using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookstoreApp.Models;
using BookstoreApp.Repositories;
using BookstoreApp.Filters;
using System.Threading.Tasks;

namespace BookstoreApp.Pages.BookManagement
{
    [TypeFilter(typeof(AuthPageFilter), Arguments = new object[] { new string[] { "Admin" } })]
    public class CreateModel : PageModel
    {
        private readonly IRepository<Book> _bookRepository;

        public CreateModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookRepository.AddAsync(Book);
            return RedirectToAction("Index", "Books");
        }
    }
}
