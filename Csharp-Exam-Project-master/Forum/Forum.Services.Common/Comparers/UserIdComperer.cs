using Forum.Models;
using System;
using System.Collections.Generic;

namespace Forum.Services.Common.Comparers
{
    public class UserIdComperer : IEqualityComparer<ForumUser>
    {
        public bool Equals(ForumUser x, ForumUser y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ForumUser obj)
        {
            throw new NotImplementedException();
        }
    }
}