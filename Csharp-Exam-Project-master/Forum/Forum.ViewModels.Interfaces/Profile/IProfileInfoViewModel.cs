using System;

namespace Forum.ViewModels.Interfaces.Profile
{
    public interface IProfileInfoViewModel
    {
        string Username { get; set; }

        DateTime RegisteredOn { get; set; }

        string Gender { get; set; }

        string Location { get; set; }

        int PostsCount { get; set; }
    }
}