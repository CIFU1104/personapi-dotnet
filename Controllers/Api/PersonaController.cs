using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaDbContext _ctx;
        public PersonaController(PersonaDbContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _ctx.personas.AsNoTracking().ToListAsync());

        [HttpGet("{cc:int}")]
        public async Task<IActionResult> GetById(int cc)
        {
            var p = await _ctx.personas.FindAsync(cc);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] persona p)
        {
            _ctx.personas.Add(p);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { cc = p.cc }, p);
        }

        [HttpPut("{cc:int}")]
        public async Task<IActionResult> Update(int cc, [FromBody] persona p)
        {
            if (cc != p.cc) return BadRequest("cc mismatch");
            _ctx.Entry(p).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(p);
        }

        [HttpDelete("{cc:int}")]
        public async Task<IActionResult> Delete(int cc)
        {
            var e = await _ctx.personas.FindAsync(cc);
            if (e is null) return NotFound();
            _ctx.personas.Remove(e);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
