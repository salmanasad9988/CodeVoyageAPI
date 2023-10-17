using CodeVoyage.Models.DTO.Category;

namespace CodeVoyage.Models.DTO.BlogPost
{
    public class BlogPostDto
    {
        public Guid Id { get; internal set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public bool IsPublic { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
