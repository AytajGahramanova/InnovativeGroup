namespace EllinMMCProject.Models
{
    public class GalleryCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<GalleryProduct> Products { get; set; }
    }
}
