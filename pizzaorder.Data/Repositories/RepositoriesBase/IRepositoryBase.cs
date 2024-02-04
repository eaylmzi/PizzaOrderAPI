using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.RepositoriesBase
{
    public interface IRepositoryBase<T> where T : class
    {
        public int Add(T entity);
        public bool Delete(Func<T, bool> method);
        public Task<T?> UpdateAsync(Func<T, bool> method, T updatedEntity);
        public Task<T?> UpdateAsync(T? entity, T updatedEntity);
        public List<T>? Get(Func<T, bool> method);
        public List<T>? GetWithPagination(Func<T, bool> method, int page, float pageResult);
        public T? GetSingle(int number);
        public T? GetSingleByMethod(Func<T, bool> method);
        public T? GetSingleByMethod(Func<T, bool> method, Func<T, bool> method2);
    }
}
