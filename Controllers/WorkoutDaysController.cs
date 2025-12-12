using System.Linq;
using System.Threading.Tasks;
using GymPlanner.Data;
using GymPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GymPlanner.Controllers
{
    public class WorkoutDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkoutDays?programId=1
        public async Task<IActionResult> Index(int? programId)
        {
            if (programId == null)
            {
                return NotFound();
            }

            ViewBag.ProgramId = programId.Value;

            var days = await _context.WorkoutDays
                .Where(d => d.TrainingProgramId == programId.Value)
                .ToListAsync();

            return View(days);
        }

        // GET: WorkoutDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.WorkoutDays
                .Include(d => d.TrainingProgram)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (day == null)
            {
                return NotFound();
            }

            ViewBag.ProgramId = day.TrainingProgramId;


            return View(day);
        }

        // GET: WorkoutDays/Create?programId=1
        [Authorize]
        public IActionResult Create(int? programId)
        {
            if (programId == null)
            {
                return NotFound();
            }

            ViewBag.ProgramId = programId.Value;

            ViewData["TrainingProgramId"] =
                new SelectList(_context.TrainingPrograms, "Id", "Title", programId.Value);

            return View();
        }

        // POST: WorkoutDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(WorkoutDay workoutDay)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProgramId = workoutDay.TrainingProgramId;

                ViewData["TrainingProgramId"] =
                    new SelectList(_context.TrainingPrograms, "Id", "Title", workoutDay.TrainingProgramId);

                return View(workoutDay);
            }

            _context.WorkoutDays.Add(workoutDay);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { programId = workoutDay.TrainingProgramId });
        }

        // GET: WorkoutDays/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.WorkoutDays.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            ViewBag.ProgramId = day.TrainingProgramId;

            ViewData["TrainingProgramId"] =
                new SelectList(_context.TrainingPrograms, "Id", "Title", day.TrainingProgramId);

            return View(day);
        }

        // POST: WorkoutDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, WorkoutDay workoutDay)
        {
            if (id != workoutDay.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ProgramId = workoutDay.TrainingProgramId;

                ViewData["TrainingProgramId"] =
                    new SelectList(_context.TrainingPrograms, "Id", "Title", workoutDay.TrainingProgramId);

                return View(workoutDay);
            }

            _context.Update(workoutDay);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { programId = workoutDay.TrainingProgramId });
        }

        // GET: WorkoutDays/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.WorkoutDays
                .Include(d => d.TrainingProgram)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (day == null)
            {
                return NotFound();
            }

            // نخزن البرنامج عشان نرجع له بعد الحذف
            ViewBag.ProgramId = day.TrainingProgramId;

            return View(day);
        }

        // POST: WorkoutDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var day = await _context.WorkoutDays.FindAsync(id);

            if (day != null)
            {
                int programId = day.TrainingProgramId;

                _context.WorkoutDays.Remove(day);
                await _context.SaveChangesAsync();

                // نرجع لنفس البرنامج
                return RedirectToAction("Index", new { programId = programId });
            }

            return NotFound();
        }
    }
}
