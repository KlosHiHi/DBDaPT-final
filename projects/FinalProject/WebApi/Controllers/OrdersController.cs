using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ShoeShopDbContext context) : ControllerBase
    {
        private readonly ShoeShopDbContext _context = context;

        // GET: api/Orders/login
        [Authorize] // Атрибут, дающий доступ лишь авторизированным пользователям
        [HttpGet("{login}")] // Метод Get, получающий заказы пользователя по логину
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByLogin(string login)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Login == login) 
                .ToListAsync() ?? null!;

            return orders is null ?
                NotFound() :
                Ok(orders);
        }

        // POST: api/Orders/5/date
        [Authorize(Roles = "admin, manager")] // Атрибут, дающий доступ к методу только пользователям с ролью admin и manager
        [HttpPut("{id}/date")] // Метод Put с изменением даты по id
        public async Task<ActionResult<Order>> PutOrderDate(int id, [FromQuery] DateOnly date)
        {
            var order = await _context.Orders.FindAsync(id); // поиск заказа по id

            if (order is null)
                return NotFound();

            order.OrderDate = date;

            try
            {
                await _context.SaveChangesAsync(); // Сохранение изменений в контексте
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Orders/5/status
        [Authorize(Roles = "admin, manager")]
        [HttpPut("{id}/status")] // Метод Put с измененим статуса заказа
        public async Task<ActionResult<Order>> PutOrderStatus(int id, [FromQuery] bool status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
                return NotFound();

            order.IsFinished = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
