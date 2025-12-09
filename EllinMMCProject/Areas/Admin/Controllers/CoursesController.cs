using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // Kursların listesi
        public IActionResult Index()
        {
            var courses = _context.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .ToList();

            return View(courses);
        }

        // Yeni kurs yarat - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kurs yarat - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, IFormFile? coverFile, List<IFormFile>? imageFiles)
        {
            if (!ModelState.IsValid)
            {
                // Əsas şəkil
                if (coverFile != null && coverFile.Length > 0)
                {
                    var extension = Path.GetExtension(coverFile.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await coverFile.CopyToAsync(stream);
                    }

                    course.Image = "/uploads/" + fileName;
                }

                // Əlavə şəkillər
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    course.Images = new List<CourseImage>();
                    foreach (var file in imageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var extension = Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString() + extension;
                            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            course.Images.Add(new CourseImage { ImageUrl = "/uploads/" + fileName });
                        }
                    }
                }

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }


        // Kurs redaktə - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // Kurs redaktə - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? file)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Əgər şəkil yüklənibsə
                    if (file != null && file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var fileName = Guid.NewGuid().ToString() + extension;
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Əsas cover şəkil yenilə
                        course.Image = "/uploads/" + fileName;

                        // Əlavə şəkil əlavə et
                        if (course.Images == null)
                            course.Images = new List<CourseImage>();

                        course.Images.Add(new CourseImage { ImageUrl = "/uploads/" + fileName });
                    }

                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.Id == course.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // Kurs detalları
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // Kurs sil - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // Kurs sil - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course != null)
            {
                // Şəkilləri serverdən də sil
                foreach (var img in course.Images)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.CourseTopics.RemoveRange(course.Topics);
                _context.CourseImages.RemoveRange(course.Images);
                _context.CourseVideos.RemoveRange(course.Videos);

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
