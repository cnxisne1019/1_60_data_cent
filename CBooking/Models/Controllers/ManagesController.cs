using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CBooking.Models;

namespace CBooking.Controllers
{
    public class ManagesController : Controller
    {
        private readonly CBookingContext _context;

        public ManagesController(CBookingContext context)
        {
            _context = context;
        }

        // GET: Manages
        public async Task<IActionResult> Index()
        {
            var cBookingContext = _context.Manage.Include(m => m.Book).Include(m => m.User);
            return View(await cBookingContext.ToListAsync());
        }

        // GET: Manages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage
                .Include(m => m.Book)
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.ManageId == id);
            if (manage == null)
            {
                return NotFound();
            }

            return View(manage);
        }

        // GET: Manages/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username");
            return View();
        }

        // POST: Manages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManageId,BookId,UserId,ManageDescription")] Manage manage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", manage.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", manage.UserId);
            return View(manage);
        }

        // GET: Manages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage.SingleOrDefaultAsync(m => m.ManageId == id);
            if (manage == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", manage.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", manage.UserId);
            return View(manage);
        }

        // POST: Manages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManageId,BookId,UserId,ManageDescription")] Manage manage)
        {
            if (id != manage.ManageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManageExists(manage.ManageId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", manage.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", manage.UserId);
            return View(manage);
        }

        // GET: Manages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage
                .Include(m => m.Book)
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.ManageId == id);
            if (manage == null)
            {
                return NotFound();
            }

            return View(manage);
        }

        // POST: Manages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manage = await _context.Manage.SingleOrDefaultAsync(m => m.ManageId == id);
            _context.Manage.Remove(manage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManageExists(int id)
        {
            return _context.Manage.Any(e => e.ManageId == id);
        }
    }
}
