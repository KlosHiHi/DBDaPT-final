using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;
using ShoeShopLibrary.Extensions;

namespace WebApp.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ShoeShopLibrary.Contexts.ShoeShopDbContext _context;

        public IndexModel(ShoeShopLibrary.Contexts.ShoeShopDbContext context)
        {
            _context = context;
        }

        public IList<OrderInfo> OrderInfo { get; set; } = default!;

        public async Task OnGetClientAsync()
        {
            var userOrders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShoeOrders)
                    .ThenInclude(s => s.Shoe)
                        .ThenInclude(s => s.Maker)
                .Where(o => o.User.UserId == int.Parse(HttpContext.Session.GetString("UserId")))
                .AsQueryable();

            IEnumerable<Order?> orderInfos = await userOrders.ToListAsync();

            OrderInfo = orderInfos.ToDtoInfos().ToList();
        }

        public async Task OnGetManagerAsync()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShoeOrders)
                    .ThenInclude(s => s.Shoe)
                        .ThenInclude(s => s.Maker)
                .AsQueryable();

            IEnumerable<Order?> orderInfos = await orders.ToListAsync();

            OrderInfo = orders.ToDtoInfos().ToList();
        }
    }
}
