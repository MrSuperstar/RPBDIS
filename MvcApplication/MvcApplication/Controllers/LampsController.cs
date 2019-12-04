using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcApplication.Data;
using MvcApplication.Models;
using MvcApplication.ViewModels;
using static MvcApplication.ViewModels.SortViewModel;

namespace MvcApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class LampsController : Controller
    {
        private readonly LightingContext _context;

        public LampsController(LightingContext context)
        {
            _context = context;
        }

        // GET: Lamps
        [ResponseCache(Duration = 20)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(string name, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;

            var lamps = await _context.Lamps.ToListAsync();
            if (!String.IsNullOrEmpty(name))
            {
                lamps = lamps.Where(p => p.LampName.Contains(name)).ToList();
            }

            var count = lamps.Count();
            IEnumerable<Lamp> items = lamps.ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.LampName).ToList();
                    break;
                case SortState.NameAsc:
                    items = items.OrderBy(s => s.LampName).ToList();
                    break;
                case SortState.LifeDesc:
                    items = items.OrderByDescending(s => s.LampLife).ToList();
                    break;
                case SortState.LifeAsc:
                    items = items.OrderBy(s => s.LampLife).ToList();
                    break;
                case SortState.PowerDesc:
                    items = items.OrderByDescending(s => s.LampPower).ToList();
                    break;
                case SortState.PowerAsc:
                    items = items.OrderBy(s => s.LampPower).ToList();
                    break;
                case SortState.IdAsc:
                    items = items.OrderBy(s => s.LampId).ToList();
                    break;
                case SortState.IdDesc:
                    items = items.OrderByDescending(s => s.LampId).ToList();
                    break;
                default:
                    items = items.OrderBy(s => s.LampName).ToList();
                    break;
            }

            items = items.Skip((page - 1) * pageSize).Take(pageSize);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                FilterViewModel = new FilterViewModel(name),
                SortViewModel = new SortViewModel(sortOrder),
                Lamps = items
            };

            return View(viewModel);
        }

        // GET: Lamps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamp = await _context.Lamps
                .FirstOrDefaultAsync(m => m.LampId == id);
            if (lamp == null)
            {
                return NotFound();
            }

            return View(lamp);
        }

        // GET: Lamps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lamps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LampId,LampName,LampPower,LampLife")] Lamp lamp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lamp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lamp);
        }

        // GET: Lamps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamp = await _context.Lamps.FindAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }
            return View(lamp);
        }

        // POST: Lamps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LampId,LampName,LampPower,LampLife")] Lamp lamp)
        {
            if (id != lamp.LampId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lamp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LampExists(lamp.LampId))
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
            return View(lamp);
        }

        // GET: Lamps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lamp = await _context.Lamps
                .FirstOrDefaultAsync(m => m.LampId == id);
            if (lamp == null)
            {
                return NotFound();
            }

            return View(lamp);
        }

        // POST: Lamps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lamp = await _context.Lamps.FindAsync(id);
            _context.Lamps.Remove(lamp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LampExists(int id)
        {
            return _context.Lamps.Any(e => e.LampId == id);
        }
    }
}
