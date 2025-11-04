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
    public class telefonoesController : Controller
    {
        private readonly PersonaDbContext _context;

        public telefonoesController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: telefonoes
        public async Task<IActionResult> Index()
        {
            var personaDbContext = _context.telefonos.Include(t => t.duenioNavigation);
            return View(await personaDbContext.ToListAsync());
        }

        // GET: telefonoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.telefonos
                .Include(t => t.duenioNavigation)
                .FirstOrDefaultAsync(m => m.num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: telefonoes/Create
        public IActionResult Create()
        {
            ViewData["duenio"] = new SelectList(_context.personas, "cc", "cc");
            return View();
        }

        // POST: telefonoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("num,oper,duenio")] telefono telefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["duenio"] = new SelectList(_context.personas, "cc", "cc", telefono.duenio);
            return View(telefono);
        }

        // GET: telefonoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }
            ViewData["duenio"] = new SelectList(_context.personas, "cc", "cc", telefono.duenio);
            return View(telefono);
        }

        // POST: telefonoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("num,oper,duenio")] telefono telefono)
        {
            if (id != telefono.num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!telefonoExists(telefono.num))
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
            ViewData["duenio"] = new SelectList(_context.personas, "cc", "cc", telefono.duenio);
            return View(telefono);
        }

        // GET: telefonoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _context.telefonos
                .Include(t => t.duenioNavigation)
                .FirstOrDefaultAsync(m => m.num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var telefono = await _context.telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.telefonos.Remove(telefono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool telefonoExists(string id)
        {
            return _context.telefonos.Any(e => e.num == id);
        }
    }
}
