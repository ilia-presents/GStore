using GStore.Data;

using GStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using GStore.Models.ViewModels;
using GStore.Models;
using Microsoft.Data.SqlClient;

namespace GStore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public bool UpdateEntity()
        {
            try
            {
                _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEntity(T entity)
        {
            try
            {
                _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool Add(T entity)
        //{
        //    _entities.Add(entity);

        //    try
        //    {
        //        _dbContext.SaveChanges();

        //        return true;
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return false;
        //    }
        //    catch (SqlException ex)
        //    {
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public async Task<bool> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);

            try
            {
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<T> GetEntityByIdAsNoTraking(int id, Expression<Func<T, bool>> predicate)
        {
            var entity = await _entities.AsNoTracking().SingleOrDefaultAsync(predicate);

            return entity;
        }

        public T GetEntityById(int id)
        {
            var entity = _entities.Find(id);

            return entity;
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            return entity;
        }

        public async Task<IEnumerable<TResult>> GetAllVmsNoTracking<TResult>(Expression<Func<T, TResult>> selector)
        {
            var resultVM = await _entities.AsNoTracking().Select(selector).ToListAsync();

            return resultVM;
        }
    }
}
