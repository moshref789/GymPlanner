using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingPrograms.ToListAsync());
        }

        // Search
        // GET: TrainingPrograms/SearchForm
        public IActionResult SearchForm()
        {
            return View();
        }

        // POST: TrainingPrograms/ShowSearchFormResult
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowSearchFormResult(string SearchProgram)
        {
            var result = await _context.TrainingPrograms
                .Where(p => p.Title.Contains(SearchProgram))
                .ToListAsync();

            return View("Index", result);
        }

        // Details
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

        // Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(TrainingProgram trainingProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(trainingProgram);
        }

        // Edit
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(trainingProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(trainingProgram);
        }

        // Delete
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingProgram = await _context.TrainingPrograms.FindAsync(id);
            if (trainingProgram != null)
            {
                _context.TrainingPrograms.Remove(trainingProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
