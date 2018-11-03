using MealTracking.Contract.Models.Base;
using MealTracking.Repository;

namespace MealTracking.Service.Common
{
    public abstract class ServiceBase<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly Repository<TEntity> Repository;

        protected ServiceBase(Repository<TEntity> repository)
        {
            Repository = repository;
        }
    }
}