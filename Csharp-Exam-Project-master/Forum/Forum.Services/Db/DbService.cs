using Forum.Data;
using Forum.Services.Interfaces.Db;

namespace Forum.Services.Db
{
    public class DbService : IDbService
    {
        public DbService(ForumDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ForumDbContext DbContext { get; }
    }
}