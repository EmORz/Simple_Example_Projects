using System.Collections.Generic;
using Forum.MapConfiguration.Contracts;
using Forum.ViewModels.Interfaces.Message;

namespace Forum.ViewModels.Message
{
    public class SendMessageInputModel : ISendMessageInputModel, IMapTo<Models.Message>
    {
        public string Description { get; set; }

        public string RecieverId { get; set; }

        public string RecieverName { get; set; }

        public IEnumerable<Models.Message> Messages { get; set; }

        public bool ShowAll { get; set; }
    }
}