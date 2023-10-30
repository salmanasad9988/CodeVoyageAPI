using Azure.Core;
using CodeVoyage.Models.Domain;
using CodeVoyage.Models.DTO.BlogPost;
using CodeVoyage.Models.DTO.Category;
using CodeVoyage.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeVoyage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;

        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostDto request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                IsPublic = request.IsPublic,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach(var categoryId in request.CategoryIds)
            {
                var exisitingCategory = await _categoryRepository.GetByIdAsync(categoryId);

                if (exisitingCategory != null)
                    blogPost.Categories.Add(exisitingCategory);
            }

            blogPost = await _blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsPublic = blogPost.IsPublic,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                    Content = blogPost.Content,
                    ImageUrl = blogPost.ImageUrl,
                    Author = blogPost.Author,
                    PublishedDate = blogPost.PublishedDate,
                    IsPublic = blogPost.IsPublic,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsPublic = blogPost.IsPublic,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrl([FromRoute] string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);

            if (blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsPublic = blogPost.IsPublic,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, [FromBody] UpdateBlogPostDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                IsPublic = request.IsPublic,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryId in request.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                    blogPost.Categories.Add(category);
            }

            var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsPublic = blogPost.IsPublic,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deletedBlogPost = await _blogPostRepository.DeleteAsync(id);

            if (deletedBlogPost == null) return NotFound();

            var response = new BlogPostDto
            {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                Description = deletedBlogPost.Description,
                Content = deletedBlogPost.Content,
                ImageUrl = deletedBlogPost.ImageUrl,
                Author = deletedBlogPost.Author,
                PublishedDate = deletedBlogPost.PublishedDate,
                IsPublic = deletedBlogPost.IsPublic,
                UrlHandle = deletedBlogPost.UrlHandle
            };

            return Ok(response);
        }
    }
}
