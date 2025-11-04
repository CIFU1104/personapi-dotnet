using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Data;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet
{
    public class estudiosController : Controller
    {
        private readonly PersonaDbContext _context;

        public estudiosController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: estudios
        public async Task<IActionResult> Index()
        {
            var personaDbContext = _context.estudios.Include(e => e.cc_perNavigation).Include(e => e.id_profNavigation);
            return View(await personaDbContext.ToListAsync());
        }

        // GET: estudios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _context.estudios
                .Include(e => e.cc_perNavigation)
                .Include(e => e.id_profNavigation)
                .FirstOrDefaultAsync(m => m.id_prof == id);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: estudios/Create
        public IActionResult Create()
        {
            ViewData["cc_per"] = new SelectList(_context.personas, "cc", "cc");
            ViewData["id_prof"] = new SelectList(_context.profesions, "id", "id");
            return View();
        }

        // POST: estudios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_prof,cc_per,fecha,univer")] estudio estudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cc_per"] = new SelectList(_context.personas, "cc", "cc", estudio.cc_per);
            ViewData["id_prof"] = new SelectList(_context.profesions, "id", "id", estudio.id_prof);
            return View(estudio);
        }

        // GET: estudios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _context.estudios.FindAsync(id);
            if (estudio == null)
            {
                return NotFound();
            }
            ViewData["cc_per"] = new SelectList(_context.personas, "cc", "cc", estudio.cc_per);
            ViewData["id_prof"] = new SelectList(_context.profesions, "id", "id", estudio.id_prof);
            return View(estudio);
        }

        // POST: estudios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_prof,cc_per,fecha,univer")] estudio estudio)
        {
            if (id != estudio.id_prof)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!estudioExists(estudio.id_prof))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["cc_per"] = new SelectList(_context.personas, "cc", "cc", estudio.cc_per);
            ViewData["id_prof"] = new SelectList(_context.profesions, "id", "id", estudio.id_prof);
            return View(estudio);
        }

        // GET: estudios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _context.estudios
                .Include(e => e.cc_perNavigation)
                .Include(e => e.id_profNavigation)
                .FirstOrDefaultAsync(m => m.id_prof == id);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudio = await _context.estudios.FindAsync(id);
            if (estudio != null)
            {
                _context.estudios.Remove(estudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool estudioExists(int id)
        {
            return _context.estudios.Any(e => e.id_prof == id);
        }
    }
}
