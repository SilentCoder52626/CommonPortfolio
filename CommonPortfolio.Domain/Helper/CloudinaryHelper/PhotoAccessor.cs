using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonPortfolio.Domain.Entity.Cloudinary;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Models.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace CommonPortfolio.Domain.Helper.CloudinaryHelper
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(CloudinarySettings config)
        {
            _cloudinary = new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
        }
        public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            if(file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if(uploadResult.Error != null)
                {
                    throw new CustomException(uploadResult.Error.Message);
                }
                return new PhotoUploadResult()
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString()
                };
            }
            throw new CustomException("File is empty");

        }

        public async Task<bool> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            if (result.Result == "ok")
                return true;
            return false;
        }
    }
}
