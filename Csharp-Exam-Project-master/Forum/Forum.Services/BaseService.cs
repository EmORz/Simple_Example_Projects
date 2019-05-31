using AutoMapper;
using Forum.Services.Interfaces.Db;
using System;

namespace Forum.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;
        protected readonly IDbService dbService;

        public BaseService(IMapper mapper, IDbService dbService)
        {
            this.mapper = mapper;
            this.dbService = dbService;
        }

        public int GetPagesCount(int postsCount)
        {
            var result = (int)Math.Ceiling(postsCount / 5.0);

            return result;
        }
    }
}