using CodeVoyage.Models.Domain;
using CodeVoyage.Models.DTO.BlogImage;
using CodeVoyage.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var blogImages = await _imageRepository.GetAllAsync();

            var response = new List<BlogImageDto>();
            foreach(var blogImage in blogImages)
            {
                response.Add(new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    FileExtension = blogImage.FileExtension,
                    UploadDate = blogImage.UploadDate,
                    Url = blogImage.Url
                });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile file,
            [FromForm] string fileName,
            [FromForm] string title)
        {
            ValidateFile(file);
            if(ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    UploadDate = DateTime.Now
                };

                blogImage = await _imageRepository.UploadAsync(file, blogImage);

                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    UploadDate = blogImage.UploadDate,
                    Url = blogImage.Url
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFile(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 5242880) //5mb
            {
                ModelState.AddModelError("file", "File size cannot be more than 5MB");
            }
        }
    }
}
