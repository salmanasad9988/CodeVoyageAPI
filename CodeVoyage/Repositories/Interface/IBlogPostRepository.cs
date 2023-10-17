using CodeVoyage.Models.Domain;

namespace CodeVoyage.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync(); 
    }
}
