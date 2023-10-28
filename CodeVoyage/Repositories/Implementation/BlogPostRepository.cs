using CodeVoyage.Data;
using CodeVoyage.Models.Domain;
using CodeVoyage.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeVoyage.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Include(x => x.Categories).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _context.BlogPosts.Include(x => x.Categories).Where(x => x.UrlHandle == urlHandle).FirstOrDefaultAsync();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var exisitingBlogPost = await _context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (exisitingBlogPost != null)
            {
                // update blogpost
                _context.Entry(exisitingBlogPost).CurrentValues.SetValues(blogPost);

                // update categories
                exisitingBlogPost.Categories = blogPost.Categories;
                
                // save changes to database
                await _context.SaveChangesAsync();

                return blogPost;
            }

            return null;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _context.BlogPosts.Remove(existingBlogPost);
                await _context.SaveChangesAsync();

                return existingBlogPost;
            }
            return null;
        }

        
    }
}
