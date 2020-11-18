using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;

namespace Winner.IRepository
{
    public interface IProductClassService
    {
        Task<int> AddAsync(ProductClass productClass);
        Task<int> DeleteOneAsync(ProductClass productClass);
        Task<int> DeleteListAsync(List<ProductClass> list);
        Task<int> EditOneAsync(ProductClass productClass);
        Task<ProductClass> GetOneAsync(int id);
        Task<List<ProductClass>> GetListAsync(List<Expression<Func<ProductClass, bool>>> wheres);
        Task<List<ProductClass>> GetListAsync(Expression<Func<ProductClass, bool>> where, int topCount);
        Task<int> GetCountAsync(List<Expression<Func<ProductClass, bool>>> wheres);
        Task<List<ProductClass>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<ProductClass, bool>>> wheres);

    }
}
