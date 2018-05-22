using HatServer.DAL;
using HatServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HatServer.Controllers
{
    public class PhraseItemsController : Controller
    {
        private IRepository<PhraseItem> _phraseItemRepository;
        private IRepository<Pack> _packRepository;

        public PhraseItemsController(IRepository<PhraseItem> phraseItemRepository, IRepository<Pack> packRepository)
        {
            _phraseItemRepository = phraseItemRepository;
            _packRepository = packRepository;
        }

        // GET: PhraseItems
        public IActionResult Index() => View(_phraseItemRepository.GetAll());

        // GET: PhraseItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _phraseItemRepository.GetAsync(id.Value);
            if (phraseItem == null)
            {
                return NotFound();
            }

            return View(phraseItem);
        }

        // GET: PhraseItems/Create
        public IActionResult Create()
        {
            ViewData["PackId"] = new SelectList(_packRepository.GetAll(), "Id", "Id");
            return View();
        }

        // POST: PhraseItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Phrase,Complexity,Description,PackId")] PhraseItem phraseItem)
        {
            if (ModelState.IsValid)
            {
                await _phraseItemRepository.InsertAsync(phraseItem);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackId"] = new SelectList(_packRepository.GetAll(), "Id", "Id", phraseItem.PackId);
            return View(phraseItem);
        }

        // GET: PhraseItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _phraseItemRepository.GetAsync(id.Value);
            if (phraseItem == null)
            {
                return NotFound();
            }
            ViewData["PackId"] = new SelectList(_packRepository.GetAll(), "Id", "Id", phraseItem.PackId);
            return View(phraseItem);
        }

        // POST: PhraseItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [NotNull] [Bind("Id,Phrase,Complexity,Description,PackId")] PhraseItem phraseItem)
        {
            if (id != phraseItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _phraseItemRepository.UpdateAsync(phraseItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhraseItemExists(phraseItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.Message = "The phrase was edited by someone else already";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackId"] = new SelectList(_packRepository.GetAll(), "Id", "Id", phraseItem.PackId);
            return View(phraseItem);
        }

        // GET: PhraseItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phraseItem = await _phraseItemRepository.GetAsync(id.Value);
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
            await _phraseItemRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PhraseItemExists(int id) => _phraseItemRepository.GetAsync(id).Result != null;
    }
}
