using CodeVoyage.Data;
using CodeVoyage.Models.Domain;
using CodeVoyage.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace CodeVoyage.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var exisitingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (exisitingCategory != null)
            {
                _context.Entry(exisitingCategory).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();

                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return existingCategory;
        }

    }
}
