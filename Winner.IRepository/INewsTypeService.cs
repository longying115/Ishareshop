using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;
using Winner.Models.Response;

namespace Winner.IRepository
{
    public interface INewsTypeService
    {
        Task<int> AddAsync(NewsType newsType);
        Task<int> DeleteOneAsync(NewsType newsType);
        Task<int> DeleteListAsync(List<NewsType> list);
        Task<int> EditOneAsync(NewsType newsType);
        Task<NewsType> GetOneAsync(int id);
        Task<List<NewsType>> GetListAsync(List<Expression<Func<NewsType, bool>>> wheres);
        Task<List<NewsType>> GetListAsync(Expression<Func<NewsType, bool>> where, int topCount);
        Task<int> GetCountAsync(List<Expression<Func<NewsType, bool>>> wheres);
        Task<List<NewsType>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<NewsType, bool>>> wheres);

    }
}
