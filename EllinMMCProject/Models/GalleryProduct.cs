using System.ComponentModel.DataAnnotations.Schema;

namespace EllinMMCProject.Models
{
    public class GalleryProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; } 
        public List<GalleryCategory> GalleryCategories { get; set; }
    }
}
