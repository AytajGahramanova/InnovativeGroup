namespace EllinMMCProject.Models
{

    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }       // Kursun adı
        public string? Description { get; set; } // Kursun qısa açıqlaması
        public string? Image { get; set; }
        // Kart cover şəkli

        // Əlaqəli məlumatlar
        public ICollection<CourseTopic> Topics { get; set; } // Mövzular
        public ICollection<CourseImage> Images { get; set; } // Sinif şəkilləri
        public ICollection<CourseVideo> Videos { get; set; } // Videolar
    }

    // Mövzular
    public class CourseTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }       // Mövzu adı
        public string Description { get; set; } // Mövzu haqqında qısa info

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

    // Sinif şəkilləri
    public class CourseImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }    // Şəkil yolu

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

    // Videolar
    public class CourseVideo
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }   // Video yolu
        public string Title { get; set; }      // Altında çıxacaq başlıq

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
