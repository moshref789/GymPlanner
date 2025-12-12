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
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises?workoutDayId=5
        public async Task<IActionResult> Index(int? workoutDayId)
        {
            if (workoutDayId == null)
            {
                return NotFound();
            }

            ViewBag.WorkoutDayId = workoutDayId.Value;

            // Get the WorkoutDay to find its ProgramId
            var workoutDay = await _context.WorkoutDays
                .FirstOrDefaultAsync(d => d.Id == workoutDayId.Value);

            if (workoutDay != null)
            {
                ViewBag.ProgramId = workoutDay.TrainingProgramId;
            }

            var exercises = await _context.Exercises
                .Where(e => e.WorkoutDayId == workoutDayId.Value)
                .ToListAsync();

            return View(exercises);
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.WorkoutDay)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            ViewBag.WorkoutDayId = exercise.WorkoutDayId;

            if (exercise.WorkoutDay != null)
            {
                ViewBag.ProgramId = exercise.WorkoutDay.TrainingProgramId;
            }


            return View(exercise);
        }

        // GET: Exercises/Create?workoutDayId=5
        [Authorize]
        public async Task<IActionResult> Create(int? workoutDayId)
        {
            if (workoutDayId == null)
            {
                return NotFound();
            }

            ViewBag.WorkoutDayId = workoutDayId.Value;

            // Get the WorkoutDay to find its ProgramId
            var workoutDay = await _context.WorkoutDays
                .FirstOrDefaultAsync(d => d.Id == workoutDayId.Value);

            if (workoutDay != null)
            {
                ViewBag.ProgramId = workoutDay.TrainingProgramId;
            }

            ViewData["WorkoutDayDropdown"] =
                new SelectList(_context.WorkoutDays, "Id", "DayName", workoutDayId.Value);

            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WorkoutDayId = exercise.WorkoutDayId;

                // Get ProgramId for the "Back" link
                var workoutDay = await _context.WorkoutDays
                    .FirstOrDefaultAsync(d => d.Id == exercise.WorkoutDayId);

                if (workoutDay != null)
                {
                    ViewBag.ProgramId = workoutDay.TrainingProgramId;
                }

                ViewData["WorkoutDayDropdown"] =
                    new SelectList(_context.WorkoutDays, "Id", "DayName", exercise.WorkoutDayId);

                return View(exercise);
            }

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { workoutDayId = exercise.WorkoutDayId });
        }

        // GET: Exercises/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.WorkoutDay)  // Include to get ProgramId
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            ViewBag.WorkoutDayId = exercise.WorkoutDayId;

            if (exercise.WorkoutDay != null)
            {
                ViewBag.ProgramId = exercise.WorkoutDay.TrainingProgramId;
            }

            ViewData["WorkoutDayDropdown"] =
                new SelectList(_context.WorkoutDays, "Id", "DayName", exercise.WorkoutDayId);

            return View(exercise);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.WorkoutDayId = exercise.WorkoutDayId;

                // Get ProgramId for the "Back" link
                var workoutDay = await _context.WorkoutDays
                    .FirstOrDefaultAsync(d => d.Id == exercise.WorkoutDayId);

                if (workoutDay != null)
                {
                    ViewBag.ProgramId = workoutDay.TrainingProgramId;
                }

                ViewData["WorkoutDayDropdown"] =
                    new SelectList(_context.WorkoutDays, "Id", "DayName", exercise.WorkoutDayId);

                return View(exercise);
            }

            _context.Update(exercise);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { workoutDayId = exercise.WorkoutDayId });
        }

        // GET: Exercises/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.WorkoutDay)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            ViewBag.WorkoutDayId = exercise.WorkoutDayId;

            if (exercise.WorkoutDay != null)
            {
                ViewBag.ProgramId = exercise.WorkoutDay.TrainingProgramId;
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise != null)
            {
                int workoutDayId = exercise.WorkoutDayId;

                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { workoutDayId = workoutDayId });
            }

            return NotFound();
        }
    }
}
