using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Profile;
using Forum.Services.UnitTests.Base;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace Forum.Services.UnitTests.Profile
{
    public class ProfileServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IProfileService profileService;

        public ProfileServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.profileService = fixture.Provider.GetService(typeof(IProfileService)) as IProfileService;

            this.SeedDb();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(users);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncatePostsTable()
        {
            var posts = this.dbService.DbContext.Posts.ToList();
            this.dbService.DbContext.Posts.RemoveRange(posts);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncatePostsTable();
            this.TruncateUsersTable();

            var user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void IsImageExtensionValid_returns_true_when_correct()
        {
            var fileName = TestsConstants.ValidTestFilename;

            var actualResult = this.profileService.IsImageExtensionValid(fileName);

            Assert.True(actualResult == true);
        }

        [Fact]
        public void IsImageExtensionValid_returns_false_when_incorrect()
        {
            var fileName = TestsConstants.InvalidTestFilename;

            var actualResult = this.profileService.IsImageExtensionValid(fileName);
            
            Assert.True(actualResult == false);
        }

        [Fact]
        public void GetProfileInfo_returns_correct_entity_when_correct()
        {
            var expectedResult = TestsConstants.TestUsername1;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", TestsConstants.TestUsername1)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.profileService.GetProfileInfo(principal);

            Assert.Equal(expectedResult, actualResult.Username);
        }
    }
}