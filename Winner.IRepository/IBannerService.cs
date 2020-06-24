using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;
using Winner.Models.Response;

namespace Winner.IRepository
{
    public interface IBannerService
    {
        Task<ResponseModel> Add(Banner banner);
        Task<ResponseModel> GetOne(int id);
        Task<ResponseModel> GetList(Expression<Func<Banner, bool>> where, int topCount);
        Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<Banner, bool>>> wheres);
        Task<ResponseModel> EditOne(Banner banner);
        Task<ResponseModel> DeleteOne(int id);

    }
}
