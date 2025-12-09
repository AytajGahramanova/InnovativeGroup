using EllinMMCProject.Models;

namespace EllinMMCProject.ViewModels
{
    public class ContactVM
    {
        
            public Appeal Appeals { get; set; } = new Appeal(); // Bu şəkildə olmalıdır
            public List<Course> Courses { get; set; }
        
    }
}
