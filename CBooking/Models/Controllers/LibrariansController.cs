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
    public class LibrariansController : Controller
    {
        private readonly CBookingContext _context;

        public LibrariansController(CBookingContext context)
        {
            _context = context;
        }

        // GET: Librarians
        public async Task<IActionResult> Index()
        {
            var cBookingContext = _context.Librarian.Include(l => l.Book).Include(l => l.User);
            return View(await cBookingContext.ToListAsync());
        }

        // GET: Librarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .Include(l => l.Book)
                .Include(l => l.User)
                .SingleOrDefaultAsync(m => m.LibrarianId == id);
            if (librarian == null)
            {
                return NotFound();
            }

            return View(librarian);
        }

        // GET: Librarians/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username");
            return View();
        }

        // POST: Librarians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibrarianId,UserId,BookId,ManageId,BorrowDay")] Librarian librarian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", librarian.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", librarian.UserId);
            return View(librarian);
        }

        // GET: Librarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian.SingleOrDefaultAsync(m => m.LibrarianId == id);
            if (librarian == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", librarian.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", librarian.UserId);
            return View(librarian);
        }

        // POST: Librarians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibrarianId,UserId,BookId,ManageId,BorrowDay")] Librarian librarian)
        {
            if (id != librarian.LibrarianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librarian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrarianExists(librarian.LibrarianId))
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
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Bookname", librarian.BookId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username", librarian.UserId);
            return View(librarian);
        }

        // GET: Librarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .Include(l => l.Book)
                .Include(l => l.User)
                .SingleOrDefaultAsync(m => m.LibrarianId == id);
            if (librarian == null)
            {
                return NotFound();
            }

            return View(librarian);
        }

        // POST: Librarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var librarian = await _context.Librarian.SingleOrDefaultAsync(m => m.LibrarianId == id);
            _context.Librarian.Remove(librarian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarianExists(int id)
        {
            return _context.Librarian.Any(e => e.LibrarianId == id);
        }
    }
}
