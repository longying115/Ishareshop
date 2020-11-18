using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;

namespace Winner.IRepository
{
    public interface IProductService
    {
        Task<int> AddAsync(Products product);
        Task<int> DeleteOneAsync(Products product);
        Task<int> DeleteListAsync(List<Products> list);
        Task<int> EditOneAsync(Products product);
        Task<Products> GetOneAsync(int id);
        Task<List<Products>> GetListAsync(List<Expression<Func<Products, bool>>> wheres);
        Task<List<Products>> GetListAsync(Expression<Func<Products, bool>> where, int topCount);
        Task<int> GetCountAsync(List<Expression<Func<Products, bool>>> wheres);
        Task<List<Products>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<Products, bool>>> wheres);

    }
}
