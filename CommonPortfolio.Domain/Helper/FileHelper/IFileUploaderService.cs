using Microsoft.AspNetCore.Http;

namespace CommonPortfolio.Domain.Helper.FileHelper
{
    public interface IFileUploaderService
    {
        Task<string> SaveFileAsync(IFormFile file, string directoryName);
        void RemoveFile(string filePath);
        void ValidateImageFiles(List<IFormFile> files);

    }
}
