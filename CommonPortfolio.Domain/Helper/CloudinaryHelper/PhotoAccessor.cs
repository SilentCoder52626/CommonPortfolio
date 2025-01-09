using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonPortfolio.Domain.Entity.Cloudinary;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Models.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPortfolio.Domain.Helper.CloudinaryHelper
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            _cloudinary = new Cloudinary(new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret));
        }
        public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            if(file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
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
