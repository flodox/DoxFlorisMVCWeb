using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoxFlorisMVCWeb.Data;
using DoxFlorisMVCWeb.Models;

namespace DoxFlorisMVCWeb.Controllers
{
    public class LidController : Controller
    {
        private readonly DoxFlorisMVCWebContext _context;

        public LidController(DoxFlorisMVCWebContext context)
        {
            _context = context;
        }

        // GET: Lid
        public async Task<IActionResult> Index()
        {
            return View(await _context.Leden.ToListAsync());
        }

        // GET: Lid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Leden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lid == null)
            {
                return NotFound();
            }

            return View(lid);
        }

        // GET: Lid/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lid/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,voornaam,achternaam,geboortedatum,straat,huisnummer,postcode,land,email,telefoon")] Lid lid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lid);
        }

        // GET: Lid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Leden.FindAsync(id);
            if (lid == null)
            {
                return NotFound();
            }
            return View(lid);
        }

        // POST: Lid/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,voornaam,achternaam,geboortedatum,straat,huisnummer,postcode,land,email,telefoon")] Lid lid)
        {
            if (id != lid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LidExists(lid.Id))
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
            return View(lid);
        }

        // GET: Lid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Leden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lid == null)
            {
                return NotFound();
            }

            return View(lid);
        }

        // POST: Lid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lid = await _context.Leden.FindAsync(id);
            _context.Leden.Remove(lid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LidExists(int id)
        {
            return _context.Leden.Any(e => e.Id == id);
        }
    }
}
