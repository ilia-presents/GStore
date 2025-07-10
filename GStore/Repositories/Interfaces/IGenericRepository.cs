using GStore.Models;
using GStore.Models.ViewModels;
using NuGet.Common;
using System.Linq;
using System.Linq.Expressions;

namespace GStore.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {


        bool UpdateEntity();

        Task<bool> UpdateAsync();
        bool UpdateEntity(T entity);

        //bool Add(T entity);
        T GetEntityById(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> AddAsync(T entity);

        Task<T> GetEntityByIdAsNoTraking(int id, Expression<Func<T, bool>> predicate);

        Task<T> GetEntityByIdAsync(int id);

        Task<IEnumerable<TResult>> GetAllVmsNoTracking<TResult>(Expression<Func<T, TResult>> selector);
    }
}
