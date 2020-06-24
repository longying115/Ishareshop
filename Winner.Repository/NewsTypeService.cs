using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Winner.Models;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

using Winner.IRepository;
using Winner.Models.Response;
using System.Linq.Expressions;

namespace Winner.Repository
{
    public class NewsTypeService : INewsTypeService
    {
        private readonly AccountContext _context;

        public NewsTypeService(AccountContext accountContext)
        {
            _context = accountContext;
        }
        public async Task<ResponseModel> Add(NewsType newsType)
        {
            _context.NewsType.Add(newsType);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "新闻分类添加成功" };
            return new ResponseModel { code = 0, result = "新闻分类添加失败" };
        }
        public async Task<ResponseModel> GetOne(int id)
        {
            NewsType newstype = await _context.NewsType.FindAsync(id);
            if (newstype == null)
                return new ResponseModel { code = 0, result = "新闻类别不存在" };
            return new ResponseModel { code = 200, result = "新闻类别获取成功", data = newstype };
        }
        public async Task<ResponseModel> GetList(Expression<Func<NewsType, bool>> where, int topCount)
        {
            var list = _context.NewsType.Where(where);
            var topdata = await list.OrderBy(s => s.sort).Take(topCount).ToListAsync();

            var response = new ResponseModel
            {
                code = 200,
                result = "新闻类别获取成功"
            };
            //foreach (var newstype in list)
            //{
            //    response.data.Add(new NewsType
            //    {
            //        id = newstype.id,
            //        sort = newstype.sort,
            //        typename = newstype.typename,
            //        keytitle = newstype.keytitle,
            //        keywords = newstype.keywords,
            //        description = newstype.description,
            //        isshow = newstype.isshow,
            //        ishead = newstype.ishead
            //    });
            //}
            //return response;//也可以尝试一下下面这种方式是否可以
            return new ResponseModel { code = 200, result = "新闻类别获取成功", data = topdata };
        }
        public async Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<NewsType, bool>>> wheres)
        {
            var list = _context.NewsType.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = list.Count();

            var pagedata = await list.OrderBy(s => s.sort).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToListAsync();

            return new ResponsePageModel { code = 200, result = "分页新闻类别获取成功", total = total, data = pagedata };
        }

        public async Task<ResponseModel> EditOne(NewsType newsType)
        {
            NewsType newsTypeEntity = await _context.NewsType.FirstOrDefaultAsync(s => s.id == newsType.id);
            if (newsTypeEntity == null)
                return new ResponseModel { code = 0, result = "新闻类别不存在" };
            newsTypeEntity.sort = newsType.sort;
            newsTypeEntity.typename = newsType.typename;
            newsTypeEntity.keytitle = newsType.keytitle;
            newsTypeEntity.keywords = newsType.keywords;
            newsTypeEntity.description = newsType.description;
            newsTypeEntity.isshow = newsType.isshow;
            newsTypeEntity.ishead = newsType.ishead;

            _context.NewsType.Update(newsTypeEntity);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "新闻类别修改成功" };
            return new ResponseModel { code = 0, result = "新闻类别修改失败" };
        }

        public async Task<ResponseModel> DeleteOne(int id)
        {
            //这里只用try-catch就可以了。
            //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。

            try
            {
                NewsType newsType = await _context.NewsType.FindAsync(id);
                if (newsType == null)
                    return new ResponseModel { code = 0, result = "新闻类别不存在" };
                _context.NewsType.Remove(newsType);
                int i = await _context.SaveChangesAsync();
                if (i > 0)
                    return new ResponseModel { code = 200, result = "新闻类别删除成功" };
                return new ResponseModel { code = 0, result = "新闻类别删除失败" };

            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }
    }
}
