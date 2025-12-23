using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ShoeShopDbContext context) : ControllerBase
    {
        private readonly ShoeShopDbContext _context = context;

        private DateOnly todayDate = DateOnly.FromDateTime(DateTime.Now);

        // GET: api/Orders/login
        [Authorize] // Атрибут, дающий доступ лишь авторизированным пользователям
        [HttpGet("{login}")] // Метод Get, получающий заказы пользователя по логину
        public async Task<ActionResult<IEnumerable<OrderDto?>>> GetOrdersByLogin(string login)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Login == login)
                .ToListAsync() ?? null!;

            return orders is null ?
                NotFound() :
                Ok(orders.ToDtos());
        }

        // POST: api/Orders/5/date
        [Authorize(Roles = "admin, manager")] // Атрибут, дающий доступ к методу только пользователям с ролью admin и manager
        [HttpPut("{id}/delivered")] // Метод Put с изменением даты доставки и статуса заказа по id
        public async Task<ActionResult<OrderDto>> PutOrderDate(int id, [FromQuery] DateOnly? deliveryDate = null, [FromQuery] bool isFinished = false)
        {
            var order = await _context.Orders.FindAsync(id); // поиск заказа по id

            if (order is null)
                return NotFound();

            if (deliveryDate is null)
                order.DeliveryDate = todayDate;
            else
                order.DeliveryDate = deliveryDate;

            order.IsFinished = isFinished;

            try
            {
                await _context.SaveChangesAsync(); // Сохранение изменений в контексте
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return order.ToDto();
        }
    }
}
