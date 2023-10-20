namespace CodeVoyage.Models.DTO.BlogPost
{
    public class UpdateBlogPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public bool IsPublic { get; set; }
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
