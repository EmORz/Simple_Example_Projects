using AutoMapper;
using CloudinaryDotNet;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Profile;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Profile;
using Forum.ViewModels.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;

namespace Forum.Services.Profile
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IOptions<CloudConfiguration> cloudConfig;

        public ProfileService(IMapper mapper, IDbService dbService, IOptions<CloudConfiguration> CloudConfig)
            : base(mapper, dbService)
        {
            cloudConfig = CloudConfig;
        }

        public IProfileInfoViewModel GetProfileInfo(ClaimsPrincipal principal)
        {
            var user =
                this.dbService
                .DbContext
                .Users
                .Include(u => u.Posts)
                .Where(u => u.UserName == principal.Identity.Name)
                .FirstOrDefault();

            var model = this.mapper.Map<ProfileInfoViewModel>(user);

            return model;
        }

        public void UploadProfilePicture(IFormFile image, string username)
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.UserName == username);

            CloudinaryDotNet.Account cloudAccount = new CloudinaryDotNet.Account(this.cloudConfig.Value.CloudName, this.cloudConfig.Value.ApiKey, this.cloudConfig.Value.ApiSecret);

            Cloudinary cloudinary = new Cloudinary(cloudAccount);

            var stream = image.OpenReadStream();

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(image.FileName, stream),
                PublicId = string.Format(ServicesConstants.CloudinaryPublicId, username)
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            string url = cloudinary.Api.UrlImgUp.BuildUrl(string.Format(ServicesConstants.CloudinaryPictureName, username));

            var updatedUrl = cloudinary.GetResource(uploadParams.PublicId).Url;

            user.ProfilePicutre = updatedUrl;

            this.dbService.DbContext.Entry(user).State = EntityState.Modified;
            this.dbService.DbContext.SaveChanges();
        }

        public bool IsImageExtensionValid(string fileName)
        {
            int counter = 0;

            foreach (var extension in ModelsConstants.AllowedImageExtensions)
            {
                if (fileName.EndsWith(extension))
                {
                    counter++;
                }
            }

            if (counter == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}