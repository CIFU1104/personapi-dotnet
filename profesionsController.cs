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
    public class profesionsController : Controller
    {
        private readonly PersonaDbContext _context;

        public profesionsController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: profesions
        public async Task<IActionResult> Index()
        {
            return View(await _context.profesions.ToListAsync());
        }

        // GET: profesions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.profesions
                .FirstOrDefaultAsync(m => m.id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: profesions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: profesions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nom,des")] profesion profesion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: profesions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: profesions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nom,des")] profesion profesion)
        {
            if (id != profesion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!profesionExists(profesion.id))
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
            return View(profesion);
        }

        // GET: profesions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.profesions
                .FirstOrDefaultAsync(m => m.id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: profesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesion = await _context.profesions.FindAsync(id);
            if (profesion != null)
            {
                _context.profesions.Remove(profesion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool profesionExists(int id)
        {
            return _context.profesions.Any(e => e.id == id);
        }
    }
}
