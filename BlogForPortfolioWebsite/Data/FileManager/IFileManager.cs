using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlogForPortfolioWebsite.Data.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
    }
}