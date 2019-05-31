using Forum.ViewModels.Interfaces.Settings;
using System;

namespace Forum.ViewModels.Settings
{
    public class UserJsonViewModel : IUserJsonViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string Gender { get; set; }

        public string Location { get; set; }
    }
}