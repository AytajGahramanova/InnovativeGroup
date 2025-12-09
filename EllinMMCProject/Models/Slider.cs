using System.ComponentModel.DataAnnotations.Schema;

namespace EllinMMCProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
      
    }
}
