using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiosController : ControllerBase
    {
        private readonly PersonaDbContext _ctx;
        public EstudiosController(PersonaDbContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _ctx.Set<estudio>().AsNoTracking().ToListAsync());

        [HttpGet("{idProf:int}/{ccPer:int}")]
        public async Task<IActionResult> GetById(int idProf, int ccPer)
        {
            var item = await _ctx.Set<estudio>().FindAsync(idProf, ccPer);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] estudio e)
        {
            _ctx.Set<estudio>().Add(e);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),
                new { idProf = e.id_prof, ccPer = e.cc_per}, e);
        }

        [HttpPut("{idProf:int}/{ccPer:int}")]
        public async Task<IActionResult> Update(int idProf, int ccPer, [FromBody] estudio e)
        {
            if (idProf != e.id_prof || ccPer != e.cc_per)
                return BadRequest("composite key mismatch");

            _ctx.Entry(e).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(e);
        }

        [HttpDelete("{idProf:int}/{ccPer:int}")]
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var entity = await _ctx.Set<estudio>().FindAsync(idProf, ccPer);
            if (entity is null) return NotFound();
            _ctx.Set<estudio>().Remove(entity);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
