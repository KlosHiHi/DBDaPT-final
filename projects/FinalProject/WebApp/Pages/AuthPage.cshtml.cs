using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class AuthPageModel : PageModel
    {
        public string UserRole => HttpContext.Session.GetString("Role");
        public bool IsAdmin => UserRole == "Администратор";

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        protected IActionResult CanEdit()
        {
            if(UserRole == "admin")
                throw new NotImplementedException();
            throw new NotImplementedException();
        }

        protected IActionResult HasRole()
        {
            if (string.IsNullOrEmpty(UserRole))
                return RedirectToPage("/Login");
            return null;
        }
        public void OnGet()
        {
        }
    }
}
