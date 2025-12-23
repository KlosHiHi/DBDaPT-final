using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Models;

namespace WebApp.Pages.Shoes
{
    public class IndexModel : PageModel
    {
        private readonly ShoeShopLibrary.Contexts.ShoeShopDbContext _context;
        private Random _random = new();
        private DateOnly _todayDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _nextWeekDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7));


        public IndexModel(ShoeShopLibrary.Contexts.ShoeShopDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string ShoeDescription { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public byte Maker { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MaxPrice
        {
            get;
            set
            {
                if (value < 0)
                    field = 0;
                else
                    field = value;
            }
        }

        [BindProperty(SupportsGet = true)]
        public bool IsInStock { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsDiscount { get; set; }

        public IList<Shoe> Shoe { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string DiscountColor { get; set; }

        [BindProperty]
        public int SelectedShoeId { get; set; }

        public async Task<IActionResult> OnPostGuestOrderAsync()
        {
            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostClientOrderAsync()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
        
            Order newOrder = new()
            {
                UserId = userId,
                OrderDate = _todayDate,
                DeliveryDate = _nextWeekDate,
                IsFinished = false,
                ReceiveCode = _random.Next(100, 999),
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            var order = _context.Entry(newOrder);
            var generatedId = order.Property(o => o.OrderId).CurrentValue;

            ShoeOrder shoeOrder = new()
            {
                ShoeId = SelectedShoeId,
                OrderId = generatedId,
                Quantity = 1
            };

            _context.ShoeOrders.Add(shoeOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task OnGetAsync()
        {
            ViewData["Makers"] = new SelectList(_context.Makers.Distinct(), "MakerId", "Name");

            var shoes = _context.Shoes
                .Include(s => s.Category)
                .Include(s => s.Maker)
                .Include(s => s.Vendor)
                .AsQueryable();

            if (!String.IsNullOrEmpty(ShoeDescription))
                shoes = shoes
                    .Where(s => s.Description
                    .Contains(ShoeDescription));

            if (Maker > 0)
                shoes = shoes
                    .Where(s => s.Maker.MakerId == Maker);

            if (MaxPrice > 0)
                shoes = shoes
                    .Where(s => s.Price <= MaxPrice);

            if (IsInStock)
                shoes = shoes.Where(s => s.Quantity > 0);

            if (IsDiscount)
                shoes = shoes.Where(s => s.Discount > 0);

            shoes = SortColumn switch
            {
                "name" => shoes.OrderBy(s => s.Category.Name),
                "vendor" => shoes.OrderBy(s => s.Vendor.Name),
                "price" => shoes.OrderBy(s => s.Price),
                "price_desc" => shoes.OrderByDescending(s => s.Price),
                _ => shoes
            };

            Shoe = await shoes.ToListAsync();
        }
    }
}
