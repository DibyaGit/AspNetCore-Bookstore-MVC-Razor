using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookstoreApp.Models;
using BookstoreApp.Repositories;
using BookstoreApp.Filters;
using System.Threading.Tasks;

namespace BookstoreApp.Pages.BookManagement
{
    [TypeFilter(typeof(AuthPageFilter), Arguments = new object[] { new string[] { "Admin" } })]
    public class EditModel : PageModel
    {
        private readonly IRepository<Book> _bookRepository;

        public EditModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookRepository.GetByIdAsync(id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookRepository.UpdateAsync(Book);
            return RedirectToAction("Index", "Books");
        }
    }
}
