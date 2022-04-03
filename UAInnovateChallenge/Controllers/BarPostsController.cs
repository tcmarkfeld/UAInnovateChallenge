#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UAInnovateChallenge.Data;

namespace UAInnovateChallenge.Models
{
    public class BarPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BarPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BarPosts.Include(b => b.Bar);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        [Route("GetBarPosts")]
        // GET: BarPosts
        public async Task<IActionResult> GetBarPosts()
        {
            var applicationDbContext = _context.BarPosts.Include(b => b.Bar);
            return Ok(await applicationDbContext.ToListAsync());
        }

        // GET: BarPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barPosts = await _context.BarPosts
                .Include(b => b.Bar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barPosts == null)
            {
                return NotFound();
            }

            return View(barPosts);
        }

        // GET: BarPosts/Create
        public IActionResult Create()
        {
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Name");
            return View();
        }

        // POST: BarPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BarId,PostDate,Post")] BarPosts barPosts)
        {
            if (ModelState.IsValid)
            {
                barPosts.Id = Guid.NewGuid();
                _context.Add(barPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Name", barPosts.BarId);
            return View(barPosts);
        }

        // GET: BarPosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barPosts = await _context.BarPosts.FindAsync(id);
            if (barPosts == null)
            {
                return NotFound();
            }
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id", barPosts.BarId);
            return View(barPosts);
        }

        // POST: BarPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BarId,PostDate,Post")] BarPosts barPosts)
        {
            if (id != barPosts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barPosts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarPostsExists(barPosts.Id))
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
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id", barPosts.BarId);
            return View(barPosts);
        }

        // GET: BarPosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barPosts = await _context.BarPosts
                .Include(b => b.Bar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barPosts == null)
            {
                return NotFound();
            }

            return View(barPosts);
        }

        // POST: BarPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var barPosts = await _context.BarPosts.FindAsync(id);
            _context.BarPosts.Remove(barPosts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarPostsExists(Guid id)
        {
            return _context.BarPosts.Any(e => e.Id == id);
        }
    }
}
