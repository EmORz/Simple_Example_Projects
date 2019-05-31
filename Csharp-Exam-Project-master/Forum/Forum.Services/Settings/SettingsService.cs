using AutoMapper;
using Forum.Models;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Message;
using Forum.Services.Interfaces.Quote;
using Forum.Services.Interfaces.Reply;
using Forum.Services.Interfaces.Report;
using Forum.Services.Interfaces.Settings;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Settings;
using Forum.ViewModels.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Forum.Services.Settings
{
    public class SettingsService : BaseService, ISettingsService
    {
        private readonly IReplyService replyService;
        private readonly IQuoteService quoteService;
        private readonly IReportService reportService;
        private readonly IMessageService messageService;
        private readonly UserManager<ForumUser> userManager;

        public SettingsService(IMapper mapper, IDbService dbService, IReplyService replyService, IQuoteService quoteService, IReportService reportService, IMessageService messageService, UserManager<ForumUser> userManager)
            : base(mapper, dbService)
        {
            this.replyService = replyService;
            this.quoteService = quoteService;
            this.reportService = reportService;
            this.messageService = messageService;
            this.userManager = userManager;
        }

        private int ChangeGender(ForumUser user, string newGender)
        {
            user.Gender = newGender;
            var result = this.dbService.DbContext.SaveChanges();

            return result;
        }

        private int ChangeLocation(ForumUser user, string newLocation)
        {
            user.Location = newLocation;
            var result = this.dbService.DbContext.SaveChanges();

            return result;
        }

        public void ChangePassword(ForumUser user, string oldPassword, string newPassword)
        {
            var result = this.userManager.ChangePasswordAsync(user, oldPassword, newPassword).GetAwaiter().GetResult();
        }

        private bool ChangeUsername(ForumUser user, string username)
        {
            var result = this.userManager.SetUserNameAsync(user, username).GetAwaiter().GetResult();

            return result.Succeeded;
        }

        public bool CheckPassword(ForumUser user, string password, ModelStateDictionary modelState)
        {
            var result = this.userManager.CheckPasswordAsync(user, password).GetAwaiter().GetResult();
            if (!result)
            {
                modelState.AddModelError("error", ErrorConstants.IncorrectUsernameOrPasswordError);
            }

            return result;
        }

        public bool DeleteAccount(ForumUser user)
        {
            this.quoteService.DeleteUserQuotes(user);

            this.replyService.DeleteUserReplies(user.UserName);

            this.reportService.DeleteUserReports(user.UserName);

            this.messageService.RemoveUserMessages(user.UserName);

            var result = this.userManager.DeleteAsync(user).GetAwaiter().GetResult();

            return result.Succeeded;
        }

        public bool EditProfile(ForumUser user, IEditProfileInputModel model)
        {
            bool isUsernameChanged = false;
            if (user.UserName == model.Username)
            {
                isUsernameChanged = true;
            }
            else
            {
                isUsernameChanged = this.ChangeUsername(user, model.Username);
            }

            var isLocationChanged = this.ChangeLocation(user, model.Location);

            var isGenderChanged = this.ChangeGender(user, model.Gender);

            if(user.UserName == model.Username && user.Gender == model.Gender && user.Location == model.Location)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEditProfileInputModel MapEditModel(string username)
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.UserName == username);

            var model = this.mapper.Map<EditProfileInputModel>(user);

            return model;
        }

        public byte[] BuildFile(ClaimsPrincipal principal)
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.UserName == principal.Identity.Name);

            var viewModel = this.mapper.Map<UserJsonViewModel>(user);

            var jsonStr = JsonConvert.SerializeObject(viewModel);

            var byteArr = Encoding.UTF8.GetBytes(jsonStr);

            return byteArr;
        }
    }
}