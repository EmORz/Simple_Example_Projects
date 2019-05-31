using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Settings;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Forum.Services.UnitTests.Settings
{
    public class SettingsServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly ISettingsService settingsService;

        public SettingsServiceTests(BaseUnitTest fixture)
        {
            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.settingsService = fixture.Provider.GetService(typeof(ISettingsService)) as ISettingsService;

            this.SeedDb();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(users);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncateUsersTable();

            var user = new ForumUser
            {
                UserName = TestsConstants.TestUsername1,
                Location = TestsConstants.TestLocation,
                Gender = TestsConstants.TestGender,
                Id = TestsConstants.TestId
            };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void EditProfile_returns_true_when_correct()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var model = new EditProfileInputModel { Username = TestsConstants.TestUsername1, Gender = TestsConstants.TestGender, Location = TestsConstants.TestLocation1 };

            var actualResult = this.settingsService.EditProfile(user, model);

            Assert.True(actualResult == true);

            this.SeedDb();  
        }

        [Fact]
        public void MapEditModel_returns_true_when_correct()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var expectedResult = this.mapper.Map<EditProfileInputModel>(user);

            var actualResult = this.settingsService.MapEditModel(user.UserName);

            Assert.Equal(expectedResult.Username, actualResult.Username);
        }

        [Fact]
        public void BuildFile_returns_true_when_correct()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var viewModel = this.mapper.Map<UserJsonViewModel>(user);

            var jsonStr = JsonConvert.SerializeObject(viewModel);

            var expectedResult = Encoding.UTF8.GetBytes(jsonStr);

            var claims = new List<Claim>
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.UserName)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var actualResult = this.settingsService.BuildFile(new ClaimsPrincipal(identity));

            Assert.Equal(expectedResult, actualResult);
        }
    }
}