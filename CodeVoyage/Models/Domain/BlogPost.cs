namespace CodeVoyage.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
