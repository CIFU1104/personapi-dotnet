using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefonoController : ControllerBase
    {
        private readonly PersonaDbContext _ctx;
        public TelefonoController(PersonaDbContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _ctx.Set<telefono>().AsNoTracking().ToListAsync());

        [HttpGet("{num}")]
        public async Task<IActionResult> GetById(string num)
        {
            var item = await _ctx.Set<telefono>().FindAsync(num);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] telefono t)
        {
            _ctx.Set<telefono>().Add(t);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { num = t.num }, t);
        }

        [HttpPut("{num}")]
        public async Task<IActionResult> Update(string num, [FromBody] telefono t)
        {
            if (!string.Equals(num, t.num, StringComparison.Ordinal))
                return BadRequest("num mismatch");

            _ctx.Entry(t).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(t);
        }

        [HttpDelete("{num}")]
        public async Task<IActionResult> Delete(string num)
        {
            var e = await _ctx.Set<telefono>().FindAsync(num);
            if (e is null) return NotFound();
            _ctx.Set<telefono>().Remove(e);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
