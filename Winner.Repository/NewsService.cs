using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Winner.Models;

using Microsoft.Extensions.Logging;

using Winner.IRepository;
using Winner.Models.Response;
using System.Linq.Expressions;

namespace Winner.Repository
{
    public class NewsService:INewsService
    {
        private readonly AccountContext _context;
        private readonly ILogger _logger;

        public NewsService(AccountContext accountContext)
        {
            _context = accountContext;
        }
        public async Task<ResponseModel> Add(News news)
        {
            _context.News.Add(news);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "新闻添加成功" };
            return new ResponseModel { code = 0, result = "新闻添加失败" };
        }
        public async Task<ResponseModel> GetOne(int id)
        {
            News news = await _context.News.FindAsync(id);
            if (news == null)
                return new ResponseModel { code = 0, result = "新闻不存在" };
            return new ResponseModel { code = 200, result = "新闻获取成功", data = news };
        }
        public async Task<ResponseModel> GetList(Expression<Func<News, bool>> where, int topCount)
        {
            var list = _context.News.Where(where);
            var topdata = await list.OrderByDescending(s => s.addtime).Take(topCount).ToListAsync();

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
            return new ResponseModel { code = 200, result = "新闻获取成功", data = topdata };
        }
        public async Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<News, bool>>> wheres)
        {
            var list = _context.News.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = list.Count();

            var pagedata = await list.OrderByDescending(s => s.addtime).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToListAsync();

            return new ResponsePageModel { code = 200, result = "分页新闻获取成功", total = total, data = pagedata };
        }
        public async Task<ResponseModel> EditOne(News news)
        {
            News newsEntity = await _context.News.FirstOrDefaultAsync(s => s.id == news.id);
            if (newsEntity == null)
                return new ResponseModel { code = 0, result = "新闻不存在" };
            newsEntity.tid = news.tid;
            newsEntity.title = news.title;
            newsEntity.keytitle = news.keytitle;
            newsEntity.keywords = news.keywords;
            newsEntity.description = news.description;
            newsEntity.author = news.author;
            newsEntity.source = news.source;
            newsEntity.smallpicture = news.smallpicture;
            newsEntity.picturetag = news.picturetag;
            newsEntity.addtime = news.addtime;
            newsEntity.hits = news.hits;
            newsEntity.lasthittime = news.lasthittime;
            newsEntity.praise = news.praise;
            newsEntity.textcontent = news.textcontent;
            newsEntity.ispicture = news.ispicture;
            newsEntity.isshow = news.isshow;
            newsEntity.ishead = news.ishead;

            _context.News.Update(newsEntity);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "新闻修改成功" };
            return new ResponseModel { code = 0, result = "新闻修改失败" };
        }
        
        public async Task<ResponseModel> DeleteOne(int id)
        {
            //这里只用try-catch就可以了。
            //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。

            try
            {
                News news = await _context.News.FindAsync(id);
                if (news == null)
                    return new ResponseModel { code = 0, result = "新闻不存在" };

                //var savePath = news.smallpicture;
                //if (!string.IsNullOrEmpty(savePath))
                //{
                //    var realyPath = Path.Combine(_hostingenvironment.WebRootPath + savePath);

                //    File.Delete(realyPath);
                //}
                //先删除图片，后删除信息
                _context.News.Remove(news);
                int i = await _context.SaveChangesAsync();
                if (i > 0)
                    return new ResponseModel { code = 200, result = "新闻删除成功" };
                return new ResponseModel { code = 0, result = "新闻删除失败" };

            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }

    }
}
