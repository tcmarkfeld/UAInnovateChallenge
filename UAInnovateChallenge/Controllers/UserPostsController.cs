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
    public class UserPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserPosts.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        [Route("GetUserPosts")]
        // GET: UserPosts
        public async Task<IActionResult> GetUserPosts()
        {
            var applicationDbContext = _context.UserPosts.Include(u => u.User);
            return Ok(await applicationDbContext.ToListAsync());
        }

        // GET: UserPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPosts = await _context.UserPosts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPosts == null)
            {
                return NotFound();
            }

            return View(userPosts);
        }

        // GET: UserPosts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PostDate,Post")] UserPosts userPosts)
        {
            if (ModelState.IsValid)
            {
                userPosts.Id = Guid.NewGuid();
                _context.Add(userPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userPosts.UserId);
            return View(userPosts);
        }

        // GET: UserPosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPosts = await _context.UserPosts.FindAsync(id);
            if (userPosts == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userPosts.UserId);
            return View(userPosts);
        }

        // POST: UserPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,PostDate,Post")] UserPosts userPosts)
        {
            if (id != userPosts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPosts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPostsExists(userPosts.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userPosts.UserId);
            return View(userPosts);
        }

        // GET: UserPosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPosts = await _context.UserPosts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPosts == null)
            {
                return NotFound();
            }

            return View(userPosts);
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPosts = await _context.UserPosts.FindAsync(id);
            _context.UserPosts.Remove(userPosts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPostsExists(Guid id)
        {
            return _context.UserPosts.Any(e => e.Id == id);
        }
    }
}
