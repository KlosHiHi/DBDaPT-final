using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;
using ShoeShopLibrary.Services;

namespace WebApp.Pages
{
    public class LoginModel(AuthService service) : PageModel
    {
        private AuthService _authService = service;

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostLoginAsync()
        {
            LoginRequest login = new(User.Login, User.Password);

            var user = await _authService.AuthUserAsync(login);
            var role = await _authService.GetUserRoleAsync(User.Login);

            if (user is null)
                return Page();

            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("SecondName", user.SecondName);
            HttpContext.Session.SetString("Patronymic", user.Patronymic);
            HttpContext.Session.SetString("Role", role.Name);

            return RedirectToPage("/Shoes/Index");
        }

        public async Task<IActionResult> OnPostGuest()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.SetString("Role", "guest");

            return RedirectToPage("/Shoes/Index");
        }
    }
}
