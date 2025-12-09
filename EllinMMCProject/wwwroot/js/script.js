// Scroll zamanı header gizlədib göstərmək funksiyası
let lastScrollTop = 0;
const navbar = document.querySelector(".header");

window.addEventListener("scroll", () => {
  let currentScroll = window.pageYOffset || document.documentElement.scrollTop;

  if (currentScroll > lastScrollTop) {
    // Aşağıya scroll etdikdə navbar gizlədilir
    navbar.style.transform = "translate(-50%,-150%)";
  } else {
    // Yuxarıya scroll etdikdə navbar göstərilir
    navbar.style.transform = "translate(-50%,0%)";
  }

 
  lastScrollTop = currentScroll <= 0 ? 0 : currentScroll;
});


// DOM hazır olduqda funksiyalar işləsin
document.addEventListener("DOMContentLoaded", () => {
 const hamburger = document.querySelector(".hamburger");
const navMenu = document.querySelector(".nav-menu");

if (hamburger && navMenu) {
  hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");

    if (navMenu.classList.contains("active")) {
      // Scroll-u blokla
      document.body.style.overflow = "hidden";
      document.documentElement.style.overflow = "hidden";
      document.body.classList.add("no-scroll");
    } else {
      // Scroll-u aç
      document.body.style.overflow = "auto";
      document.documentElement.style.overflow = "auto";
      document.body.classList.remove("no-scroll");
    }
  });
}




  // Statistik rəqəmlərin animasiyası
  function animateCounter(element, target, duration = 2000) {
    let start = 0;
    const increment = target / (duration / 16);

    function updateCounter() {
      start += increment;
      if (start < target) {
        element.textContent = Math.floor(start);
        requestAnimationFrame(updateCounter);
      } else {
        element.textContent = target;
      }
    }
    updateCounter();
  }


  // Intersection Observer ilə animasiyalar
  const observerOptions = {
    threshold: 0.1,
    rootMargin: "0px 0px -50px 0px",
  };

  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        // Statistik rəqəmlər üçün animasiya
        if (entry.target.classList.contains("stat-number")) {
          const target = parseInt(entry.target.getAttribute("data-target"), 10);
          setTimeout(() => {
            animateCounter(entry.target, target);
          }, Math.random() * 500); // Təsadüfi gecikmə ilə sıra effekti
        }

        // Müxtəlif elementlər üçün fərqli animasiyalar
        if (entry.target.classList.contains("course-card")) {
          entry.target.style.animation = "fadeInUp 0.6s ease-out forwards";
        }

        if (entry.target.classList.contains("advantage-item")) {
          entry.target.style.animation = "bounceIn 0.8s ease-out forwards";
        }

        if (entry.target.classList.contains("testimonial-card")) {
          entry.target.style.animation = "slideInLeft 0.6s ease-out forwards";
        }

        // Element görünən olduqda 'visible' klassı əlavə et
        entry.target.classList.add("visible");
      }
    });
  }, observerOptions);


  // Qalereya filtrləmə funksiyası
  const filterButtons = document.querySelectorAll(".filter-btn");
  const galleryItems = document.querySelectorAll(".gallery-item");

  filterButtons.forEach((button) => {
    button.addEventListener("click", () => {
      // Bütün düymələrdən active sinifini sil
      filterButtons.forEach((btn) => btn.classList.remove("active"));
      // Kliklənmiş düyməyə active sinifi əlavə et
      button.classList.add("active");

      const filterValue = button.getAttribute("data-filter");

      galleryItems.forEach((item) => {
        if (filterValue === "all" || item.getAttribute("data-category") === filterValue) {
          item.style.display = "block";
          setTimeout(() => {
            item.style.opacity = "1";
            item.style.transform = "scale(1)";
          }, 100);
        } else {
          item.style.opacity = "0";
          item.style.transform = "scale(0.8)";
          setTimeout(() => {
            item.style.display = "none";
          }, 300);
        }
      });
    });
  });


  // FAQ akordeon funksiyası
  const faqQuestions = document.querySelectorAll(".faq-question");

  faqQuestions.forEach((question) => {
    question.addEventListener("click", () => {
      const faqItem = question.parentElement;
      const isActive = faqItem.classList.contains("active");

      // Bütün FAQ elementlərini bağla
      document.querySelectorAll(".faq-item").forEach((item) => {
        item.classList.remove("active");
      });

      // Əgər kliklənmiş element aktiv deyilsə, aktiv et
      if (!isActive) {
        faqItem.classList.add("active");
      }
    });
  });


  

  // Anchor linklər üçün hamar scroll effekti
  document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
    anchor.addEventListener("click", (e) => {
      e.preventDefault();
      const target = document.querySelector(anchor.getAttribute("href"));
      if (target) {
        target.scrollIntoView({
          behavior: "smooth",
          block: "start",
        });
      }
    });
  });


  // Sticky header üçün fon rəngi dəyişmə
  if (navbar) {
    window.addEventListener("scroll", () => {
      if (window.scrollY > 100) {
        navbar.style.background = "rgba(30, 58, 138, 0.95)";
        navbar.style.backdropFilter = "blur(10px)";
      } else {
        navbar.style.background = "#1e3a8a";
        navbar.style.backdropFilter = "none";
      }
    });
  }


  // Şəkillər üçün yüklənmə animasiyası
  const images = document.querySelectorAll("img");
  images.forEach((img) => {
    // Şəkil yüklənəndə opacity artırılır
    img.addEventListener("load", () => {
      img.style.opacity = "1";
    });

    // Başlanğıc opacity və keçid tərtibatı
    img.style.opacity = "0";
    img.style.transition = "opacity 0.3s ease";

    // Şəkil artıq yüklənibsə, opacity artır
    if (img.complete) {
      img.style.opacity = "1";
    }
  });


  // "Yuxarı qayıt" düyməsi yaradılması
  const backToTopBtn = document.createElement("button");
  backToTopBtn.innerHTML = '<i class="fas fa-arrow-up"></i>';
  backToTopBtn.className = "back-to-top";
  backToTopBtn.style.cssText = `
    position: fixed;
    bottom: 20px;
    right: 20px;
    background: #5b21b6;
    color: white;
    border: none;
    border-radius: 50%;
    width: 50px;
    height: 50px;
    cursor: pointer;
    opacity: 0;
    visibility: hidden;
    transition: all 0.3s ease;
    z-index: 1000;
    box-shadow: 0 2px 10px rgba(0,0,0,0.2);
  `;

  document.body.appendChild(backToTopBtn);

  // Scroll olduqda düymənin göstərilməsi/gizlənməsi
  window.addEventListener("scroll", () => {
    if (window.scrollY > 300) {
      backToTopBtn.style.opacity = "1";
      backToTopBtn.style.visibility = "visible";
    } else {
      backToTopBtn.style.opacity = "0";
      backToTopBtn.style.visibility = "hidden";
    }
  });

  // Düyməyə kliklə yuxarı scroll et
  backToTopBtn.addEventListener("click", () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  });


  // Hero bölməsində parallax effekti
  const heroSection = document.querySelector(".hero-slider");
  if (heroSection) {
    window.addEventListener("scroll", () => {
      const scrolled = window.pageYOffset;
      const rate = scrolled * -0.5;
      heroSection.style.transform = `translateY(${rate}px)`;
    });
  }

  // Body-yə "loaded" sinifi əlavə et
  document.body.classList.add("loaded");

  // Animasiya üçün elementləri müşahidə et
  ["course-card", "advantage-item", "testimonial-card", "stat-number"].forEach((cls) => {
    document.querySelectorAll(`.${cls}`).forEach((el) => observer.observe(el));
  });

  // Hover effektləri - kurs kartları
  document.querySelectorAll(".course-card").forEach((card) => {
    card.addEventListener("mouseenter", () => {
      card.style.transform = "translateY(-10px) scale(1.02)";
      card.style.boxShadow = "0 15px 35px rgba(124, 58, 237, 0.2)";
    });

    card.addEventListener("mouseleave", () => {
      card.style.transform = "translateY(0) scale(1)";
      card.style.boxShadow = "0 5px 15px rgba(0, 0, 0, 0.1)";
    });
  });

  // Floating animasiyası - ikonlar üçün
  document.querySelectorAll(".advantage-icon, .stat-icon").forEach((icon) => {
    icon.style.animation = "float 3s ease-in-out infinite";
  });
});


// Köməkçi funksiya: debounce (təkrar çağırmanı gecikdirir)
function debounce(func, wait, immediate) {
  let timeout;
  return function executedFunction(...args) {
    const later = () => {
      timeout = null;
      if (!immediate) func(...args);
    };
    const callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
    if (callNow) func(...args);
  };
}

// Pəncərə ölçüsü dəyişəndə responsive menyu bağla
window.addEventListener(
  "resize",
  debounce(() => {
    const navMenu = document.querySelector(".nav-menu");
    const hamburger = document.querySelector(".hamburger");

    if (window.innerWidth > 768) {
      if (navMenu) navMenu.classList.remove("active");
      if (hamburger) hamburger.classList.remove("active");
      document.body.style.overflow = "auto"; // scroll-u aç
    }
  }, 250),
);



// Print üçün stil əlavə və çıxar
window.addEventListener("beforeprint", () => {
  document.body.classList.add("printing");
});

window.addEventListener("afterprint", () => {
  document.body.classList.remove("printing");
});


// CSS animasiyalarını dinamik əlavə et
const animationStyles = `
@keyframes slideInLeft {
  from {
    opacity: 0;
    transform: translateX(-50px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes slideInRight {
  from {
    opacity: 0;
    transform: translateX(50px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes bounceIn {
  0% {
    opacity: 0;
    transform: scale(0.3);
  }
  50% {
    opacity: 1;
    transform: scale(1.05);
  }
  70% {
    transform: scale(0.9);
  }
  100% {
    opacity: 1;
    transform: scale(1);
  }
}

@keyframes pulse {
  0% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.05);
  }
  100% {
    transform: scale(1);
  }
}

.course-card:hover {
  animation: pulse 0.6s ease-in-out;
}

.advantage-item:hover {
  animation: bounceIn 0.6s ease-in-out;
}
`;

// Animasiyaları səhifəyə əlavə et
const styleSheet = document.createElement("style");
styleSheet.textContent = animationStyles;
document.head.appendChild(styleSheet);


// Float animasiyası üçün keyframes əlavə et
const floatingAnimation = `
@keyframes float {
  0% {
    transform: translateY(0px);
  }
  50% {
    transform: translateY(-10px);
  }
  100% {
    transform: translateY(0px);
  }
}
`;

const floatingStyleSheet = document.createElement("style");
floatingStyleSheet.textContent = floatingAnimation;
document.head.appendChild(floatingStyleSheet);

//esas sehifenin scrol dizayni
