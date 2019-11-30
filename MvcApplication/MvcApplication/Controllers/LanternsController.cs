using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcApplication.Data;
using MvcApplication.Models;
using MvcApplication.ViewModels;

namespace MvcApplication.Controllers
{
    public class LanternsController : Controller
    {
        private readonly LightingContext _context;

        public LanternsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Lanterns
        [ResponseCache(Duration = 20)]
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int pageSize = 10;

            var lanterns = _context.Lanterns.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                lanterns = lanterns.Where(p => p.LanternName.Contains(name)).ToList();
            }

            var count = lanterns.Count();
            IEnumerable<Lantern> items = lanterns.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                FilterViewModel = new FilterViewModel(name),
                Lanterns = lanterns
            };

            return View(viewModel);
        }

        // GET: Lanterns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lantern = await _context.Lanterns
                .Include(l => l.Lamp)
                .FirstOrDefaultAsync(m => m.LanternId == id);
            if (lantern == null)
            {
                return NotFound();
            }

            return View(lantern);
        }

        // GET: Lanterns/Create
        public IActionResult Create()
        {
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId");
            return View();
        }

        // POST: Lanterns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanternId,LampId,LanternName,LanternType,IsOperable")] Lantern lantern)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lantern);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", lantern.LampId);
            return View(lantern);
        }

        // GET: Lanterns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lantern = await _context.Lanterns.FindAsync(id);
            if (lantern == null)
            {
                return NotFound();
            }
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", lantern.LampId);
            return View(lantern);
        }

        // POST: Lanterns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanternId,LampId,LanternName,LanternType,IsOperable")] Lantern lantern)
        {
            if (id != lantern.LanternId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lantern);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanternExists(lantern.LanternId))
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
            ViewData["LampId"] = new SelectList(_context.Lamps, "LampId", "LampId", lantern.LampId);
            return View(lantern);
        }

        // GET: Lanterns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lantern = await _context.Lanterns
                .Include(l => l.Lamp)
                .FirstOrDefaultAsync(m => m.LanternId == id);
            if (lantern == null)
            {
                return NotFound();
            }

            return View(lantern);
        }

        // POST: Lanterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lantern = await _context.Lanterns.FindAsync(id);
            _context.Lanterns.Remove(lantern);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanternExists(int id)
        {
            return _context.Lanterns.Any(e => e.LanternId == id);
        }
    }
}
