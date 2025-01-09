
using CommonPortfolio.Domain.Models.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace CommonPortfolio.Domain.Helper.CloudinaryHelper
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<bool> DeletePhoto(string publicId);
    }
}
