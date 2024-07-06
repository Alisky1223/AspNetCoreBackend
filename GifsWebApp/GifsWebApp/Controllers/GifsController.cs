using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GifsWebApp.Data;
using GifsWebApp.Models;
using System.Diagnostics;

namespace GifsWebApp.Controllers
{
    public class GifsController : Controller
    {
        private readonly GifsWebAppContext _context;

        public GifsController(GifsWebAppContext context)
        {
            _context = context;
        }

        // GET: Gifs
        public async Task<IActionResult> Index()
        {
              return _context.Gif != null ? View(await _context.Gif.ToListAsync()) : Problem("Entity set 'GifsWebAppContext.Gif'  is null.");
        }

        // GET: ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
              return View();
        }
        // Post: Gifs/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string searchPhrase)
        {
            return _context.Gif != null ?
                        View("Index",await _context.Gif.Where(j => j.GifName.Contains(searchPhrase)).ToListAsync()) :
                        Problem("Entity set 'GifsWebAppContext.Gif'  is null.");
        }

        // GET: Gifs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gif == null)
            {
                return NotFound();
            }

            var gif = await _context.Gif
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gif == null)
            {
                return NotFound();
            }

            return View(gif);
        }

        // GET: Gifs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GifName,GifDiscription")] Gif gif) // Removed Id from Bind
        {
            if (ModelState.IsValid)
            {
                var gifId = await _context.Gif.CountAsync();
                gifId += 1;
                var mygif = new Gif
                {
                    Id = gifId,
                    GifName = gif.GifName,
                    GifDiscription = gif.GifDiscription
                };
                _context.Add(mygif);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gif);
        }

        // GET: Gifs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gif == null)
            {
                return NotFound();
            }

            var gif = await _context.Gif.FindAsync(id);
            if (gif == null)
            {
                return NotFound();
            }
            return View(gif);
        }

        // POST: Gifs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GifName,GifDiscription")] Gif gif)
        {
            if (id != gif.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gif);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GifExists(gif.Id))
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
            return View(gif);
        }

        // GET: Gifs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gif == null)
            {
                return NotFound();
            }

            var gif = await _context.Gif
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gif == null)
            {
                return NotFound();
            }

            return View(gif);
        }

        // POST: Gifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gif == null)
            {
                return Problem("Entity set 'GifsWebAppContext.Gif'  is null.");
            }
            var gif = await _context.Gif.FindAsync(id);
            if (gif != null)
            {
                _context.Gif.Remove(gif);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GifExists(int id)
        {
          return (_context.Gif?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
