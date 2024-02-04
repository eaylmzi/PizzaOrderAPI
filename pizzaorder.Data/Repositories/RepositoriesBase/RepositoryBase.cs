using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.RepositoriesBase
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        PizzaOrderDBContext _context = new PizzaOrderDBContext();

        private DbSet<T> query { get; set; }
        public RepositoryBase()
        {
            query = _context.Set<T>();
        }
        //Adds the given entity to the _context and saves the changes(SaveChanges method).
        //Checks the Id property of the added entity, and if it is successfully added, returns the Id value, otherwise it returns -1.
        public int Add(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                var id = _context.Entry(entity).Property("Id").CurrentValue;

                if(id != null){
                    return (int)id;
                }               
            }
            return -1;
        }
        // Queries for an entity that satisfies the given condition, removes it if found, and saves the changes.
        // Returns true if the entity was deleted, or false if no entity was found.
        public bool Delete(Func<T, bool> method)
        {
            T? entity = query
                     .Where(method)
                     .SingleOrDefault();
            if (entity != null)
            {
                query.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        // Queries for entities that satisfy the given condition, selects them, and returns a list.
        public List<T>? Get(Func<T, bool> method)
        {
            var list = query
                      .Where(method)
                      .ToList();
            return list;
        }

        public List<T>? GetWithPagination(Func<T, bool> method, int page, float pageResult)
        {
            var pageCount = Math.Ceiling(query.Where(method).Count() / pageResult);
            var list = query
                      .Where(method)
                      .Skip((page - 1) * (int)pageResult)
                      .Take((int)pageResult)
                      .ToList();
            return list;
        }
        // Finds and returns a single entity based on the provided unique identifier.
        public T? GetSingle(int number)
        {
            return query.Find(number);
        }
        // Queries for an entity that satisfies the given condition and returns it. 
        public T? GetSingleByMethod(Func<T, bool> method)
        {
            try
            {
                T? entity = query
                            .Where(method)
                            .SingleOrDefault();
                if(entity != null)
                {
                    return entity;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        // Queries for an entity that satisfies both given conditions and returns it.
        public T? GetSingleByMethod(Func<T, bool> method, Func<T, bool> method2)
        {
            try
            {
                T? entity = query
                      .AsEnumerable()
                      .Where(m => method(m) && method2(m))
                      .SingleOrDefault();
                if (entity != null)
                {
                    return entity;
                }
                return default(T);
            }
            catch (Exception)
            {
                return null;
            }
        }
        // Queries for an entity that satisfies the given condition, updates it with the provided entity's values,
        // and asynchronously saves the changes. Returns the updated entity or null if the entity is not found.
        public async Task<T?> UpdateAsync(Func<T, bool> metot, T updatedEntity)
        {
            try
            {
                T? entity = query
                     .Where(metot)
                     .SingleOrDefault();

                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                    await _context.SaveChangesAsync();
                    return updatedEntity;
                }

                return default(T);

            }
            catch (Exception)
            {
                return null;
            }
        }
        //  If the provided entity is not null, updates it with the provided values and asynchronously saves the changes.
        //  Returns the updated entity or null if the provided entity is null or not found.
        public async Task<T?> UpdateAsync(T? entity, T updatedEntity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                    await _context.SaveChangesAsync();
                    return updatedEntity;
                }

                return default(T);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
