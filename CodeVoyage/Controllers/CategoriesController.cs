using CodeVoyage.Models;
using CodeVoyage.Models.Domain;
using CodeVoyage.Models.DTO.Category;
using CodeVoyage.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodeVoyage.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto request)
        {
            var category = new Category
            {
                Name = request.Name
            };

            await _categoryRepository.CreateAsync(category);

            var response = new CreateCategoryDto
            {
                Name = request.Name
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name
            };

            category = await _categoryRepository.UpdateAsync(category);

            if(category == null)
                return NotFound();

            var response = new UpdateCategoryDto
            {
                Name = category.Name
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id) 
        {
            var category = await _categoryRepository.DeleteAsync(id);

            if (category == null)
                return NotFound();

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(response);
        }

    }
}
