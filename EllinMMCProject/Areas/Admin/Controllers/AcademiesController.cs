using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EllinMMCProject.DAL;
using EllinMMCProject.Models;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcademiesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public AcademiesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Academies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Academy.Include(x=>x.academyTopics).ToListAsync());
        }

        // GET: Admin/Academies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academy = await _context.Academy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academy == null)
            {
                return NotFound();
            }

            return View(academy);
        }

        // GET: Admin/Academies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Academies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Image,formFile")] Academy academy)
        {
            if (ModelState.IsValid)
            {

                if (academy.formFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + academy.formFile.FileName;

                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await academy.formFile.CopyToAsync(stream);
                    }

                    academy.Image = "/uploads/" + uniqueFileName;
                }
                _context.Add(academy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academy);
        }

        // GET: Admin/Academies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academy = await _context.Academy.FindAsync(id);
            if (academy == null)
            {
                return NotFound();
            }
            return View(academy);
        }

        // POST: Admin/Academies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image")] Academy academy)
        {
            if (id != academy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademyExists(academy.Id))
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
            return View(academy);
        }

        // GET: Admin/Academies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academy = await _context.Academy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academy == null)
            {
                return NotFound();
            }

            return View(academy);
        }

        // POST: Admin/Academies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academy = await _context.Academy.FindAsync(id);
            if (academy != null)
            {
                _context.Academy.Remove(academy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademyExists(int id)
        {
            return _context.Academy.Any(e => e.Id == id);
        }
    }
}
