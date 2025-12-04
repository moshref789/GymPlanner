using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymPlanner.Data;
using GymPlanner.Models;
using Microsoft.AspNetCore.Authorization;

namespace GymPlanner.Controllers
{
    public class TrainingProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: TrainingPrograms/Trainer
        // GET: TrainingPrograms/SearchForm
        public IActionResult SearchForm()
        {
            return View();
        }


        // GET: TrainingPrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingPrograms.ToListAsync());
        }

        // GET: TrainingPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
            
        }
        // POST: TrainingPrograms/ShowSearchFormResult
        [HttpPost]
        public async Task<IActionResult> ShowSearchFormResult(string SearchProgram)
        {
            if (_context.TrainingPrograms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TrainingPrograms' is null.");
            }

            var result = await _context.TrainingPrograms
                .Where(p => p.Title.Contains(SearchProgram))
                .ToListAsync();

            // نرجع View Index مع النتائج
            return View("Index", result);
        }


        // POST: TrainingPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] TrainingProgram trainingProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingPrograms.FindAsync(id);
            if (trainingProgram == null)
            {
                return NotFound();
            }
            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingProgramExists(trainingProgram.Id))
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
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _context.TrainingPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingProgram = await _context.TrainingPrograms.FindAsync(id);
            if (trainingProgram != null)
            {
                _context.TrainingPrograms.Remove(trainingProgram);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingProgramExists(int id)
        {
            return _context.TrainingPrograms.Any(e => e.Id == id);
        }
    }
}
