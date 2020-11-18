using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;
using Winner.Models.Response;

namespace Winner.IRepository
{
    public interface INewsService
    {
        Task<int> AddAsync(News news);
        Task<int> DeleteOneAsync(News news);
        Task<int> DeleteListAsync(List<News> list);
        Task<int> EditOneAsync(News news);
        Task<News> GetOneAsync(int id);
        Task<List<News>> GetListAsync(List<Expression<Func<News, bool>>> wheres);
        Task<List<News>> GetListAsync(Expression<Func<News, bool>> where, int topCount);
        Task<int> GetCountAsync(List<Expression<Func<News, bool>>> wheres);
        Task<List<News>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<News, bool>>> wheres);
        
        

    }
}
