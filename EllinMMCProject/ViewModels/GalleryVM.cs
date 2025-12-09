using EllinMMCProject.Models;

namespace EllinMMCProject.ViewModels
{
    public class GalleryVM
    {
        public List<GalleryCategory> GalleryCategories { get; set; }
        public List<GalleryProduct> GalleryProducts { get; set; }
        public List<Story> Stories { get; set; }
    }
}
