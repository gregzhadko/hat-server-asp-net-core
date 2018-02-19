using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HatServer.Data;
using HatServer.Models;

namespace HatServer.Controllers
{
    public class PhraseItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhraseItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhraseItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhraseItem.ToListAsync());
        }

        // GET: PhraseItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _context.PhraseItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (phraseItem == null)
            {
                return NotFound();
            }

            return View(phraseItem);
        }

        // GET: PhraseItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhraseItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Phrase,Complexity,Description")] PhraseItem phraseItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phraseItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phraseItem);
        }

        // GET: PhraseItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _context.PhraseItem.SingleOrDefaultAsync(m => m.Id == id);
            if (phraseItem == null)
            {
                return NotFound();
            }
            return View(phraseItem);
        }

        // POST: PhraseItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Phrase,Complexity,Description")] PhraseItem phraseItem)
        {
            if (id != phraseItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phraseItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhraseItemExists(phraseItem.Id))
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
            return View(phraseItem);
        }

        // GET: PhraseItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _context.PhraseItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (phraseItem == null)
            {
                return NotFound();
            }

            return View(phraseItem);
        }

        // POST: PhraseItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phraseItem = await _context.PhraseItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.PhraseItem.Remove(phraseItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhraseItemExists(int id)
        {
            return _context.PhraseItem.Any(e => e.Id == id);
        }
    }
}
