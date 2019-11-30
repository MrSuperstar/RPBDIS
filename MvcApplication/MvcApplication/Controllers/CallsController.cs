using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcApplication.Data;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class CallsController : Controller
    {
        private readonly LightingContext _context;

        public CallsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Calls
        [ResponseCache(Duration = 20)]
        public async Task<IActionResult> Index()
        {
            var lightingContext = _context.Calls.Include(c => c.Lantern).Include(c => c.Master).Include(c => c.Section);
            return View(await lightingContext.Take(20).ToListAsync());
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Lantern)
                .Include(c => c.Master)
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // GET: Calls/Create
        public IActionResult Create()
        {
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId");
            ViewData["MasterId"] = new SelectList(_context.Masters, "MasterId", "MasterId");
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId");
            return View();
        }

        // POST: Calls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,MasterId,SectionId,LanternId,DateCall")] Call call)
        {
            if (ModelState.IsValid)
            {
                _context.Add(call);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", call.LanternId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "MasterId", "MasterId", call.MasterId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", call.SectionId);
            return View(call);
        }

        // GET: Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", call.LanternId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "MasterId", "MasterId", call.MasterId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", call.SectionId);
            return View(call);
        }

        // POST: Calls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallId,MasterId,SectionId,LanternId,DateCall")] Call call)
        {
            if (id != call.CallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(call);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallExists(call.CallId))
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
            ViewData["LanternId"] = new SelectList(_context.Lanterns, "LanternId", "LanternId", call.LanternId);
            ViewData["MasterId"] = new SelectList(_context.Masters, "MasterId", "MasterId", call.MasterId);
            ViewData["SectionId"] = new SelectList(_context.Sections, "SectionId", "SectionId", call.SectionId);
            return View(call);
        }

        // GET: Calls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Lantern)
                .Include(c => c.Master)
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallExists(int id)
        {
            return _context.Calls.Any(e => e.CallId == id);
        }
    }
}
