using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GraduatesController : Controller
    {
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly AppDbContext _context;

        public GraduatesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Graduates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Graduate.ToListAsync());
        }

        // GET: Admin/Graduates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduate = await _context.Graduate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graduate == null)
            {
                return NotFound();
            }

            return View(graduate);
        }

        // GET: Admin/Graduates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Graduates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Position,Image,formFile")] Graduate graduate)
        {
            if (ModelState.IsValid)
            {
				if (graduate.formFile != null)
				{
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

					if (!Directory.Exists(uploadFolder))
						Directory.CreateDirectory(uploadFolder);

					string uniqueFileName = Guid.NewGuid().ToString() + "_" + graduate.formFile.FileName;

					string filePath = Path.Combine(uploadFolder, uniqueFileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await graduate.formFile.CopyToAsync(stream);
					}

					graduate.Image = "/uploads/" + uniqueFileName;
				}
				_context.Add(graduate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(graduate);
        }

        // GET: Admin/Graduates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduate = await _context.Graduate.FindAsync(id);
            if (graduate == null)
            {
                return NotFound();
            }
            return View(graduate);
        }

        // POST: Admin/Graduates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Position,Image")] Graduate graduate)
        {
            if (id != graduate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graduate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraduateExists(graduate.Id))
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
            return View(graduate);
        }

        // GET: Admin/Graduates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduate = await _context.Graduate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graduate == null)
            {
                return NotFound();
            }

            return View(graduate);
        }

        // POST: Admin/Graduates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graduate = await _context.Graduate.FindAsync(id);
            if (graduate != null)
            {
                _context.Graduate.Remove(graduate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraduateExists(int id)
        {
            return _context.Graduate.Any(e => e.Id == id);
        }
    }
}
