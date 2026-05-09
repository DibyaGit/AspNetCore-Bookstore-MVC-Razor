using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookstoreApp.Models;
using BookstoreApp.Repositories;
using System.Threading.Tasks;

namespace BookstoreApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        public RegisterModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewUser.Role = "User"; // Default role
            await _userRepository.AddAsync(NewUser);

            return RedirectToPage("/Account/Login");
        }
    }
}
