using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Role;
using Microsoft.AspNetCore.Identity;

namespace Forum.ViewModels.Role
{
    public class UserRoleViewModel : IMapFrom<IdentityUserRole<string>>, IUserRoleViewModel
    {
        public string UserId { get; set; }

        public ForumUser User { get; set; }

        public string RoleId { get; set; }

        public IdentityRole Role { get; set; }
    }
}