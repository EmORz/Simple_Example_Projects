namespace Forum.Web.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        //Finish implementing Repository pattern
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}