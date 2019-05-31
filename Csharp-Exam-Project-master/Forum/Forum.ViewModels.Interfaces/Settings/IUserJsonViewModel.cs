using Forum.MapConfiguration.Contracts;
using Forum.Models;
using System;

namespace Forum.ViewModels.Interfaces.Settings
{
    public interface IUserJsonViewModel : IMapFrom<ForumUser>
    {
        string Id { get; set; }

        string Username { get; set; }

        DateTime RegisteredOn { get; set; }

        string Gender { get; set; }

        string Location { get; set; }
    }
}