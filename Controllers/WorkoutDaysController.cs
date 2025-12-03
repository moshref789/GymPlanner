using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Data;
using GymPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.Controllers
{
    public class WorkoutDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkoutDays
        public async Task<IActionResult> Index(int? programId)
        {
            IQueryable<WorkoutDay> query = _context.WorkoutDays
                                                   .Include(d => d.TrainingProgram);

            if (programId.HasValue)
            {
                query = query.Where(d => d.TrainingProgramId == programId.Value);
                ViewBag.ProgramId = programId.Value;
            }

            return View(await query.ToListAsync());
        }


        // GET: WorkoutDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var day = await _context.WorkoutDays
                                    .Include(d => d.TrainingProgram)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (day == null)
                return NotFound();

            return View(day);
        }

        // GET: WorkoutDays/Create
        public IActionResult Create(int? programId)
        {
            ViewData["TrainingProgramId"] =
                new SelectList(_context.TrainingPrograms, "Id", "Title", programId);

            return View();
        }


        // POST: WorkoutDays/Create
        // POST: WorkoutDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutDay workoutDay)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TrainingProgramId"] =
                    new SelectList(_context.TrainingPrograms, "Id", "Title", workoutDay.TrainingProgramId);

                return View(workoutDay);
            }

            _context.WorkoutDays.Add(workoutDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: WorkoutDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var day = await _context.WorkoutDays.FindAsync(id);
            if (day == null)
                return NotFound();

            ViewData["TrainingProgramId"] =
                new SelectList(_context.TrainingPrograms, "Id", "Title", day.TrainingProgramId);

            return View(day);
        }

        // POST: WorkoutDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,DayName,TrainingProgramId")] WorkoutDay workoutDay)
        {
            if (id != workoutDay.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.WorkoutDays.Any(e => e.Id == workoutDay.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["TrainingProgramId"] =
                new SelectList(_context.TrainingPrograms, "Id", "Title", workoutDay.TrainingProgramId);

            return View(workoutDay);
        }

        // GET: WorkoutDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var day = await _context.WorkoutDays
                                    .Include(d => d.TrainingProgram)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (day == null)
                return NotFound();

            return View(day);
        }

        // POST: WorkoutDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var day = await _context.WorkoutDays.FindAsync(id);
            if (day != null)
                _context.WorkoutDays.Remove(day);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
