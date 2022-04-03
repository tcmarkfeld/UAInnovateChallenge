#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UAInnovateChallenge.Data;

namespace UAInnovateChallenge.Models
{
    public class UserFavoritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFavorites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserFavorites.Include(u => u.Bar).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        [Route("GetUserFavorites")]
        // GET: UserFavorites
        public async Task<IActionResult> GetUserFavorites()
        {
            var applicationDbContext = _context.UserFavorites.Include(u => u.Bar).Include(u => u.User);
            return Ok(await applicationDbContext.ToListAsync());
        }

        // GET: UserFavorites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorites = await _context.UserFavorites
                .Include(u => u.Bar)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFavorites == null)
            {
                return NotFound();
            }

            return View(userFavorites);
        }

        // GET: UserFavorites/Create
        public IActionResult Create()
        {
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserFavorites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BarId,IsFavorite")] UserFavorites userFavorites)
        {
            if (ModelState.IsValid)
            {
                userFavorites.Id = Guid.NewGuid();
                _context.Add(userFavorites);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id", userFavorites.BarId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userFavorites.UserId);
            return View(userFavorites);
        }

        // GET: UserFavorites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorites = await _context.UserFavorites.FindAsync(id);
            if (userFavorites == null)
            {
                return NotFound();
            }
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id", userFavorites.BarId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userFavorites.UserId);
            return View(userFavorites);
        }

        // POST: UserFavorites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,BarId,IsFavorite")] UserFavorites userFavorites)
        {
            if (id != userFavorites.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFavorites);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFavoritesExists(userFavorites.Id))
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
            ViewData["BarId"] = new SelectList(_context.Bar, "Id", "Id", userFavorites.BarId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userFavorites.UserId);
            return View(userFavorites);
        }

        // GET: UserFavorites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorites = await _context.UserFavorites
                .Include(u => u.Bar)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFavorites == null)
            {
                return NotFound();
            }

            return View(userFavorites);
        }

        // POST: UserFavorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userFavorites = await _context.UserFavorites.FindAsync(id);
            _context.UserFavorites.Remove(userFavorites);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFavoritesExists(Guid id)
        {
            return _context.UserFavorites.Any(e => e.Id == id);
        }
    }
}
