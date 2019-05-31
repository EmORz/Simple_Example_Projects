using Forum.Models;
using Forum.ViewModels.Interfaces.Role;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Role
{
    public interface IRoleService
    {
        IEnumerable<IUserRoleViewModel> GetUsersRoles(int start);

        int Promote(ForumUser user);

        int Demote(ForumUser user);

        IEnumerable<IUserRoleViewModel> SearchForUsers(string str);
    }
}