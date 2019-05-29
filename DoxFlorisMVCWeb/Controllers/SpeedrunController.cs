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

namespace DoxFlorisMVCWeb.Controllers
{
    public class SpeedrunController : Controller
    {
        private readonly DoxFlorisMVCWebContext _context;

        public SpeedrunController(DoxFlorisMVCWebContext context)
        {
            _context = context;
        }

        // GET: Speedrun
        public async Task<IActionResult> Index()
        {
            var viewModel = new ListSpeedrunViewModel();
            viewModel.Speedruns = await _context.Speedruns.Include(s => s.lid).ToListAsync();
            return View(viewModel);
        }

        // GET: Speedrun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speedrun = await _context.Speedruns
                .Include(s => s.lid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speedrun == null)
            {
                return NotFound();
            }

            return View(speedrun);
        }

        // GET: Speedrun/Create
        public IActionResult Create()
        {
            var viewModel = new CreateSpeedrunViewModels();
            viewModel.Speedrun = new Speedrun();       
            viewModel.Leden = new SelectList(_context.Leden, "Id", "Id");
            return View(viewModel);
        }

        // POST: Speedrun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpeedrunViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Speedrun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.Leden= new SelectList(_context.Leden, "Id", "Id");
            return View(viewModel);
        }

        // GET: Speedrun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speedrun = await _context.Speedruns.FindAsync(id);
            if (speedrun == null)
            {
                return NotFound();
            }
            ViewData["lidId"] = new SelectList(_context.Leden, "Id", "Id", speedrun.lidId);
            return View(speedrun);
        }

        // POST: Speedrun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,snelheid,datum,plank,zeil,lidId")] Speedrun speedrun)
        {
            if (id != speedrun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speedrun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeedrunExists(speedrun.Id))
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
            ViewData["lidId"] = new SelectList(_context.Leden, "Id", "Id", speedrun.lidId);
            return View(speedrun);
        }

        // GET: Speedrun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speedrun = await _context.Speedruns
                .Include(s => s.lid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speedrun == null)
            {
                return NotFound();
            }

            return View(speedrun);
        }

        // POST: Speedrun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speedrun = await _context.Speedruns.FindAsync(id);
            _context.Speedruns.Remove(speedrun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeedrunExists(int id)
        {
            return _context.Speedruns.Any(e => e.Id == id);
        }
    }
}
