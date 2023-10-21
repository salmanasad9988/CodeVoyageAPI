namespace CodeVoyage.Models.DTO.BlogImage
{
    public class BlogImageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

    }
}
