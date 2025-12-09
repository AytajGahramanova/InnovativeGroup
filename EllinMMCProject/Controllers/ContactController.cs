using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using EllinMMCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EllinMMCProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ContactVM contactVM = new ContactVM
            {
                Appeals = new Appeal(), // Yeni boş Appeal yarat
                Courses = _db.Courses?.ToList() ?? new List<Course>() // Null check əlavə et
            };
            return View(contactVM);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactVM contactVM, string selectCourse)
        {
            // Courses listini yenidən yüklə
            contactVM.Courses = _db.Courses?.ToList() ?? new List<Course>();

            if (!ModelState.IsValid)
            {
                // Yeni Appeal obyekti yarat
                var newAppeal = new Appeal
                {
                    FullName = contactVM.Appeals?.FullName,
                    Email = contactVM.Appeals?.Email,
                    Phone = contactVM.Appeals?.Phone,
                    Course = selectCourse,
                    Message = contactVM.Appeals?.Message
                };

                await _db.Appeal.AddAsync(newAppeal);
                await _db.SaveChangesAsync();

                return RedirectToAction("SuccessMessage", new { course = selectCourse });
            }

            // Əgər model valid deyilsə, formu yenidən göstər
            return View("Index", contactVM);
        }

        public IActionResult SuccessMessage(string course)
        {
            ViewBag.SelectCourse = course;
            return View();
        }
    }
}