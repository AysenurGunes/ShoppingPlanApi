using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShoppingPlanApi.DataAccess
{
    public class ShoppingPlanRepositoryBase<TEntity, TContext> : IShoppingPlan<TEntity>
        where TEntity : class, new()
        where TContext : ShoppingPlanDbContext , new()
    {
        private readonly ShoppingPlanDbContext _dbContext;
        public ShoppingPlanRepositoryBase()
        {
            _dbContext =new TContext();
        }
        public int Add(TEntity entity)
        {
            try
            {

                _dbContext.Entry(entity).State = EntityState.Added;
                _dbContext.SaveChanges();
                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {

                return StatusCodes.Status400BadRequest;
            }
        }

        public int Delete(TEntity entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public int Edit(TEntity entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public IList<TEntity> GetAll()
        {
            try
            {
                return _dbContext.Set<TEntity>().ToList();

            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

        public TEntity GetByID(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return _dbContext.Set<TEntity>().Where(expression).FirstOrDefault();
            }
            catch (Exception)
            {
                return new TEntity();
            }
        }
        public IList<TEntity> GetSpecial(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return _dbContext.Set<TEntity>().Where(expression).ToList();
            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

    }
}
