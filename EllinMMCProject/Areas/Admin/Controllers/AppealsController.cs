using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppealsController : Controller
    {
        private readonly AppDbContext _db;
        public AppealsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Appeal> appeals = _db.Appeal.ToList();
            return View(appeals);
        }
    }
}
