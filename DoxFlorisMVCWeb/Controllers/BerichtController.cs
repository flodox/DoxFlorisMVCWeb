using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoxFlorisMVCWeb.Data;
using DoxFlorisMVCWeb.Models;
using DoxFlorisMVCWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DoxFlorisMVCWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BerichtController : Controller
    {
        private readonly DoxFlorisMVCWebContext _context;

        public BerichtController(DoxFlorisMVCWebContext context)
        {
            _context = context;
        }

        // GET: Bericht
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            var viewModel = new ListBerichtViewModel();
            viewModel.Berichten = await _context.Berichten.Include(b => b.lid).ToListAsync();
            return View(viewModel);
        }

        // GET: Bericht/Details/5
        [AllowAnonymous]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Berichten
                .Include(b => b.lid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bericht == null)
            {
                return NotFound();
            }

            return View(bericht);
        }

        // GET: Bericht/Create
        
        public IActionResult Create()
        {
            var viewModel = new CreateBerichtViewModel();
            viewModel.Bericht = new Bericht();
            viewModel.Leden = new SelectList(_context.Leden, "Id", "voornaam");
            return View(viewModel);
        }

        // POST: Bericht/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBerichtViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Bericht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.Leden = new SelectList(_context.Leden, "Id", "Id");
            return View(viewModel);
        }

        // GET: Bericht/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Berichten.FindAsync(id);
            if (bericht == null)
            {
                return NotFound();
            }
            ViewData["lidId"] = new SelectList(_context.Leden, "Id", "Id", bericht.lidId);
            return View(bericht);
        }

        // POST: Bericht/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,titel,inhoud,datum,lidId")] Bericht bericht)
        {
            if (id != bericht.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bericht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BerichtExists(bericht.Id))
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
            ViewData["lidId"] = new SelectList(_context.Leden, "Id", "Id", bericht.lidId);
            return View(bericht);
        }

        // GET: Bericht/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Berichten
                .Include(b => b.lid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bericht == null)
            {
                return NotFound();
            }

            return View(bericht);
        }

        // POST: Bericht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bericht = await _context.Berichten.FindAsync(id);
            _context.Berichten.Remove(bericht);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BerichtExists(int id)
        {
            return _context.Berichten.Any(e => e.Id == id);
        }
    }
}
