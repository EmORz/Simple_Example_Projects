using Forum.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Interfaces.Db
{
    public interface IDbService
    {
        ForumDbContext DbContext { get; }
    }
}
