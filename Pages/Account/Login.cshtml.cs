using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookstoreApp.Models;
using BookstoreApp.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public LoginModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
