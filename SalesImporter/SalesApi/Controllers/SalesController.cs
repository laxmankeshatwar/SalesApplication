using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Data;
using SalesApi.Models;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public SalesController(SalesDbContext context)
        {
            _context = context;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _context.Sales.ToListAsync();
            return Ok(sales);
        }

        // GET: api/sales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> CreateSale(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSale),
                new { id = sale.Id },
                sale);
        }

        // PUT: api/sales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, Sale sale)
        {
            if (id != sale.Id)
                return BadRequest();

            _context.Entry(sale).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
                return NotFound();

            _context.Sales.Remove(sale);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}