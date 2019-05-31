using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Role;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Interfaces.Role;
using Forum.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Role
{
    public class RoleServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IRoleService roleService;

        public RoleServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.roleService = fixture.Provider.GetService(typeof(IRoleService)) as IRoleService;

            this.SeedDb();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(users);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateRolesTable()
        {
            var roles = this.dbService.DbContext.Roles.ToList();
            this.dbService.DbContext.Roles.RemoveRange(roles);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateUserRolesTable()
        {
            var userRoles = this.dbService.DbContext.UserRoles.ToList();
            this.dbService.DbContext.UserRoles.RemoveRange(userRoles);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncateRolesTable();
            this.TruncateUserRolesTable();
            this.TruncateUsersTable();

            var user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };
            var secondUser = new ForumUser { Id = TestsConstants.TestId1, UserName = TestsConstants.TestUsername2 };
            var thirdUser = new ForumUser { Id = TestsConstants.TestId2, UserName = TestsConstants.TestUsername3 };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.Users.Add(secondUser);
            this.dbService.DbContext.Users.Add(thirdUser);
            this.dbService.DbContext.SaveChanges();

            var ownerRole = new IdentityRole { Id = TestsConstants.TestId1, Name = Common.Role.Owner };
            var adminRole = new IdentityRole { Id = TestsConstants.TestId2, Name = Common.Role.Administrator };
            var userRole = new IdentityRole { Id = TestsConstants.TestId3, Name = Common.Role.User };

            this.dbService.DbContext.Roles.Add(ownerRole);
            this.dbService.DbContext.Roles.Add(adminRole);
            this.dbService.DbContext.Roles.Add(userRole);
            this.dbService.DbContext.SaveChanges();

            for (int i = 0; i < this.dbService.DbContext.Users.Count(); i++)
            {
                var currentRoleId = this.dbService.DbContext.Roles.ToList()[i].Id;
                var currentuserId = this.dbService.DbContext.Users.ToList()[i].Id;

                var newUserRole = new IdentityUserRole<string> { RoleId = currentRoleId, UserId = currentuserId };
                this.dbService.DbContext.UserRoles.Add(newUserRole);
                this.dbService.DbContext.SaveChanges();
            }
        }

        [Fact]
        public void GetUsersRoles_returns_correct_list_with_entities()
        {
            var expectedResult =
                this.dbService
                .DbContext
                .UserRoles
                .Where(ur => ur.RoleId != TestsConstants.TestId1)
                .Select(ur => this.mapper.Map<UserRoleViewModel>(ur))
                .ToList();

            foreach (var ur in expectedResult)
            {
                ur.User = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == ur.UserId);
                ur.Role = this.dbService.DbContext.Roles.Where(r => r.Id == ur.RoleId).FirstOrDefault();
            }

            expectedResult =
                expectedResult
                .Where(ur => ur.Role.Name != Common.Role.Owner)
                .OrderBy(ur => ur.User.UserName)
                .Skip(0)
                .Take(5)
                .ToList();

            var actualResult = this.roleService.GetUsersRoles(0);

            Assert.Equal(expectedResult.Count(), actualResult.Count());
        }

        [Fact]
        public void SearchForUsers_returns_correct_list_with_entities()
        {
            var expectedResult = new List<IUserRoleViewModel>();

            var actualResult = this.roleService.SearchForUsers("g");

            Assert.Equal(expectedResult, actualResult);
        }
    }
}