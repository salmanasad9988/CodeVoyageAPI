using CodeVoyage.Models.Domain;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeVoyage.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<IEnumerable<BlogImage>> GetAllAsync();
        Task<BlogImage> UploadAsync(IFormFile file, BlogImage blogImage);
    }
}
