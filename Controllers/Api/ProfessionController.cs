using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionController : ControllerBase
    {
        private readonly PersonaDbContext _ctx;
        public ProfesionController(PersonaDbContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _ctx.Set<profesion>().AsNoTracking().ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _ctx.Set<profesion>().FindAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] profesion p)
        {
            _ctx.Set<profesion>().Add(p);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = p.id }, p);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] profesion p)
        {
            if (id != p.id) return BadRequest("id mismatch");
            _ctx.Entry(p).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(p);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _ctx.Set<profesion>().FindAsync(id);
            if (e is null) return NotFound();
            _ctx.Set<profesion>().Remove(e);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
