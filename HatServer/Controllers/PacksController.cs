using HatServer.DAL;
using HatServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatServer.Controllers
{
    //[Route("api/[controller]")]
    //public class PacksController : ControllerBase
    //{
    //    private IRepository<Pack> _packRepository;

    //    public PacksController(IRepository<Pack> packRepository)
    //    {
    //        _packRepository = packRepository;
    //    }

    //    [HttpGet]
    //    public List<Pack> GetAll() => _packRepository.GetAll().ToList();

    //    [HttpGet("{id}", Name = "Get")]
    //    public async Task<IActionResult> Get(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var pack = await _packRepository.GetAsync(id.Value);
    //        if (pack == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(pack);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create([FromBody] Pack item)
    //    {
    //        if (item == null)
    //        {
    //            return BadRequest();
    //        }

    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest();
    //        }

    //        await _packRepository.InsertAsync(item);
    //        return CreatedAtRoute("Get", new { id = item.Id }, item);
    //    }


        //// GET: Packs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pack = await _packRepository.GetAsync(id.Value);
        //    if (pack == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(pack);
        //}

        //// POST: Packs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Language,Name,Description")] Pack pack)
        //{
        //    if (id != pack.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _packRepository.UpdateAsync(pack);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PackExists(pack.Id))
        //            {
        //                return NotFound();
        //            }

        //            throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(pack);
        //}

        //// GET: Packs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pack = await _packRepository.GetAsync(id.Value);
        //    if (pack == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pack);
        //}

        //// POST: Packs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _packRepository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PackExists(int id) => _packRepository.GetAsync(id).Result != null;
    //}
}
