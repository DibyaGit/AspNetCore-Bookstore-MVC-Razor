using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookstoreApp.Models;
using BookstoreApp.Repositories;
using BookstoreApp.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApp.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Book> _bookRepository;

        public IndexModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotalAmount => CartItems.Sum(item => item.Book.Price * item.Quantity);

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.Book.Id == bookId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { Book = book, Quantity = 1 });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int bookId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.Book.Id == bookId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToPage();
        }
    }
}
