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
        Task<ResponseModel> Add(NewsType newsType);
        Task<ResponseModel> GetOne(int id);
        Task<ResponseModel> GetList(Expression<Func<NewsType, bool>> where, int topCount);
        Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<NewsType, bool>>> wheres);
        Task<ResponseModel> EditOne(NewsType newsType);
        Task<ResponseModel> DeleteOne(int id);

    }
}
