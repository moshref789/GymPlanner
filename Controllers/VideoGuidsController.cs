using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymPlanner.Data;
using GymPlanner.Models;

namespace GymPlanner.Controllers
{
    public class VideoGuidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoGuidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoGuids
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoGuid.ToListAsync());
        }

        // GET: VideoGuids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGuid = await _context.VideoGuid
                .FirstOrDefaultAsync(m => m.id == id);
            if (videoGuid == null)
            {
                return NotFound();
            }

            return View(videoGuid);
        }

        // GET: VideoGuids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoGuids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Muscle,Description,VideoUrl")] VideoGuid videoGuid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoGuid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoGuid);
        }

        // GET: VideoGuids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGuid = await _context.VideoGuid.FindAsync(id);
            if (videoGuid == null)
            {
                return NotFound();
            }
            return View(videoGuid);
        }

        // POST: VideoGuids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Muscle,Description,VideoUrl")] VideoGuid videoGuid)
        {
            if (id != videoGuid.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoGuid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoGuidExists(videoGuid.id))
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
            return View(videoGuid);
        }

        // GET: VideoGuids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGuid = await _context.VideoGuid
                .FirstOrDefaultAsync(m => m.id == id);
            if (videoGuid == null)
            {
                return NotFound();
            }

            return View(videoGuid);
        }

        // POST: VideoGuids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoGuid = await _context.VideoGuid.FindAsync(id);
            if (videoGuid != null)
            {
                _context.VideoGuid.Remove(videoGuid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoGuidExists(int id)
        {
            return _context.VideoGuid.Any(e => e.id == id);
        }

        public async Task<IActionResult> SearchVideo()
        {
            return View();
        }

        public async Task<IActionResult> SearchResults(string SearchString)
        {
            if(_context.VideoGuid == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VideoGuid'  is null.");
            }
            var results = await _context.VideoGuid
                                        .Where(v => v.Muscle.Contains(SearchString))
                                        .ToListAsync();
            return View("Index", results);
        }
    }
}
