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
        Task<ResponseModel> Add(News news);
        Task<ResponseModel> GetOne(int id);
        Task<ResponseModel> GetList(Expression<Func<News, bool>> where, int topCount);
        Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<News, bool>>> wheres);
        Task<ResponseModel> EditOne(News news);
        Task<ResponseModel> DeleteOne(int id);

    }
}
