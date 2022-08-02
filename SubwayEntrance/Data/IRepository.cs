using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SubwayEntrance.Data
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<T> Add(T entity);
        Task<T> FindByCondition(Expression<Func<T, bool>> expression);
       
       
    }
}
