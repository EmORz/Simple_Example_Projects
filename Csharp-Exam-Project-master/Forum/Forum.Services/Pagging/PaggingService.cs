using Forum.Services.Interfaces.Pagging;
using System;

namespace Forum.Services.Pagging
{
    public class PaggingService : IPaggingService
    {
        public int GetPagesCount(int postsCount)
        {
            var result = (int)Math.Ceiling(postsCount / 5.0);

            return result;
        }
    }
}