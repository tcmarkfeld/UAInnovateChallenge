#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UAInnovateChallenge.Data;

namespace UAInnovateChallenge.Models
{
    public class BarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bar.ToListAsync());
        }

        public async Task<IActionResult> GetBarPhoto(Guid id)
        {
            var bar = await _context.Bar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }
            var imageData = bar.BarPicture;

            return File(imageData, "image/jpg");
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // GET: Bars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cover,Bio,WaitTime,City,Street,ZipCode,State")] Bar bar, IFormFile BarPicture)
        {
            if (ModelState.IsValid)
            {
                if (BarPicture != null && BarPicture.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await BarPicture.CopyToAsync(memoryStream);
                    bar.BarPicture = memoryStream.ToArray();
                }
                bar.Id = Guid.NewGuid();
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            return View(bar);
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Cover,Bio,WaitTime,City,Street,ZipCode,State")] Bar bar, IFormFile BarPicture)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (BarPicture != null && BarPicture.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await BarPicture.CopyToAsync(memoryStream);
                        bar.BarPicture = memoryStream.ToArray();
                    }
                    else //grab EXISTING photo from DB in case user didn't select a new one
                    {
                        Bar existingProduct = _context.Bar.AsNoTracking().FirstOrDefault(m => m.Id == id);
                        if (existingProduct != null)
                        {
                            bar.BarPicture = existingProduct.BarPicture;
                        }
                    }
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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
            return View(bar);
        }

        // GET: Bars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _context.Bar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bar = await _context.Bar.FindAsync(id);
            _context.Bar.Remove(bar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarExists(Guid id)
        {
            return _context.Bar.Any(e => e.Id == id);
        }
    }
}
