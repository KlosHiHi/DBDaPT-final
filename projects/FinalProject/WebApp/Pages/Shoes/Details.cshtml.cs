using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Models;

namespace WebApp.Pages.Shoes
{
    public class DetailsModel : PageModel
    {
        private readonly ShoeShopLibrary.Contexts.ShoeShopDbContext _context;

        public DetailsModel(ShoeShopLibrary.Contexts.ShoeShopDbContext context)
        {
            _context = context;
        }

        public Shoe Shoe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoes.FirstOrDefaultAsync(m => m.ShoeId == id);

            if (shoe is not null)
            {
                Shoe = shoe;

                return Page();
            }

            return NotFound();
        }
    }
}
