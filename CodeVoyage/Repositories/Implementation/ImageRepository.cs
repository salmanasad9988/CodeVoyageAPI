using CodeVoyage.Data;
using CodeVoyage.Models.Domain;
using CodeVoyage.Repositories.Interface;

namespace CodeVoyage.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public ImageRepository(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads\\Images", $"{blogImage.FileName}");

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            blogImage.Url = @$"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Uploads/Images/{blogImage.FileName}";

            await _context.BlogImages.AddAsync(blogImage);
            await _context.SaveChangesAsync();

            return blogImage; 
        }
    }
}
