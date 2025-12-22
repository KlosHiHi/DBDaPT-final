using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Models;

namespace WebApp.Pages.Shoes
{
    public class CreateModel : PageModel
    {
        private readonly ShoeShopLibrary.Contexts.ShoeShopDbContext _context;

        public CreateModel(ShoeShopLibrary.Contexts.ShoeShopDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
        ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "Name");
        ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name");
            return Page();
        }

        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shoes.Add(Shoe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
