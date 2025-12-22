using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController(ShoeShopDbContext context) : ControllerBase
    {
        private readonly ShoeShopDbContext _context = context;

        // GET: api/Shoes
        [AllowAnonymous]
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Shoe>>> GetShoes()
            => await _context.Shoes.ToListAsync(); //Метод Get на получение всех товаров из БД

        // GET: api/Shoes/А112Т4
        [AllowAnonymous]
        [HttpGet("{article}")] // Метод Get на получение товара по артикулу
        public async Task<ActionResult<Shoe>> GetShoe(string article)
        {
            var shoe = await _context.Shoes.FirstOrDefaultAsync(s => s.Article == article);

            return shoe is null ?
                NotFound() :
                Ok(shoe);
        }

        // PUT: api/Shoes/5
        [Authorize(Roles = "admin, manager")] // метод доступен только пользователям с ролью admin и manager
        [HttpPut("{id}")] //изменение товара по id
        public async Task<IActionResult> PutShoe(int id, Shoe shoe)
        {
            if (id != shoe.ShoeId)
            {
                return BadRequest();
            }

            _context.Entry(shoe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShoeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Shoes
        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public async Task<ActionResult<Shoe>> PostShoe(Shoe shoe)
        {
            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoe), new { id = shoe.ShoeId }, shoe);
        }

        // DELETE: api/Shoes/5
        [Authorize(Roles = "admin, manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe is null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ShoeExists(int id)
        {
            return await _context.Shoes.AnyAsync(s => s.ShoeId == id);
        }
    }
}
