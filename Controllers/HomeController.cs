using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IlanSitesiEF.Data;
using IlanSitesiEF.Models;
using Slugify;

namespace IlanSitesiEF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            SlugHelper helper = new SlugHelper();

            string title = "Hellog dünya";
            string slug = helper.GenerateSlug(title);
            //return Content(slug);

              return _context.IlanModel != null ? 
                          View(await _context.IlanModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.IlanModel'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IlanModel == null)
            {
                return NotFound();
            }

            var ilanModel = await _context.IlanModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilanModel == null)
            {
                return NotFound();
            }

            return View(ilanModel);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Slug,Title,Details,Price,Created_on,Updated_on,Images")] IlanModel ilanModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilanModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ilanModel);
        }

        public IActionResult GorselYukle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GorselYukle(IFormFile image)
        {
            if (image == null)
            {
                return Content("Foto nerde ?");

            }
            string filePathAndName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\user_upload", DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + image.Name);

            using (var stream = new FileStream(filePathAndName, FileMode.Create))
            {
                image.CopyTo(stream);
                return Content("Başarılı");
            }
               
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IlanModel == null)
            {
                return NotFound();
            }

            var ilanModel = await _context.IlanModel.FindAsync(id);
            if (ilanModel == null)
            {
                return NotFound();
            }
            return View(ilanModel);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Slug,Title,Details,Price,Created_on,Updated_on,Images")] IlanModel ilanModel)
        {
            if (id != ilanModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilanModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlanModelExists(ilanModel.Id))
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
            return View(ilanModel);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IlanModel == null)
            {
                return NotFound();
            }

            var ilanModel = await _context.IlanModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilanModel == null)
            {
                return NotFound();
            }

            return View(ilanModel);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IlanModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IlanModel'  is null.");
            }
            var ilanModel = await _context.IlanModel.FindAsync(id);
            if (ilanModel != null)
            {
                _context.IlanModel.Remove(ilanModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlanModelExists(int id)
        {
          return (_context.IlanModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
