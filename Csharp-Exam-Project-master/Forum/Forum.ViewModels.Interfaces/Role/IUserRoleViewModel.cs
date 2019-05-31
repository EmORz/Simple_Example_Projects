using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.ViewModels.Interfaces.Role
{
    public interface IUserRoleViewModel 
    {
        string UserId { get; set; }

        ForumUser User { get; set; }
    
        string RoleId { get; set; }

        IdentityRole Role { get; set; }
    }
}