using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Winner.Models;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

using System.IO;

using Winner.IRepository;

namespace Winner.Repository
{
    public class ProductService:IProductService
    {
        private readonly AccountContext _context;
        private readonly ILogger _logger;
        private IHostingEnvironment _hostingenvironment;

        public ProductService(AccountContext accountcontext)
        {
            _context = accountcontext;
        }
        public async Task<IEnumerable<Products>> GetList(int? page, int? pagesize)
        {
            var productentiters = from s in _context.Products.Where(s => s.isshow == true)
                               select s;

            //排序
            productentiters = productentiters.OrderByDescending(s => s.addtime);

            //分页
            if (page != null && page > 0)
            {
                if (pagesize == null || pagesize <= 0)
                {
                    pagesize = 8;
                }

                productentiters = productentiters.Skip(Convert.ToInt32((page - 1) * pagesize)).Take(Convert.ToInt32(pagesize));
            }
            IEnumerable<Products> productlist = await productentiters.ToListAsync();

            return productlist;
        }
        public async Task<News> Get(int id)
        {
            News news = await _context.News.SingleOrDefaultAsync(s => s.id == id);

            return news;
        }
        public async Task<int> Post(News news)
        {
            try
            {
                _context.News.Add(news);

                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 204;
            }
        }
        public async Task<int> Put(int id, News news)
        {
            try
            {
                News newsentity = await _context.News.SingleOrDefaultAsync(s => s.id == id);
                if (newsentity != null)
                {
                    newsentity.tid = news.tid;
                    newsentity.title = news.title;
                    newsentity.keytitle = news.keytitle;
                    newsentity.keywords = news.keywords;
                    newsentity.description = news.description;
                    newsentity.author = news.author;
                    newsentity.source = news.source;
                    newsentity.smallpicture = news.smallpicture;
                    newsentity.picturetag = news.picturetag;
                    newsentity.addtime = news.addtime;
                    newsentity.hits = news.hits;
                    newsentity.lasthittime = news.lasthittime;
                    newsentity.praise = news.praise;
                    newsentity.textcontent = news.textcontent;
                    newsentity.ispicture = news.ispicture;
                    newsentity.isshow = news.isshow;
                    newsentity.ishead = news.ishead;

                    _context.News.Update(newsentity);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    return 404;
                }

            }
            catch
            {
                return 204;
            }
        }

        public async Task<int> Delete(int id)
        {
            //这里只用try-catch就可以了。
            //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。
            try
            {
                var newsentity = await _context.News.SingleOrDefaultAsync(s => s.id == id);

                if (newsentity == null)
                {
                    return 404;
                }
                else
                {
                    var savePath = newsentity.smallpicture;
                    if (!string.IsNullOrEmpty(savePath))
                    {
                        var realyPath = Path.Combine(_hostingenvironment.WebRootPath + savePath);

                        File.Delete(realyPath);
                    }
                    //先删除图片，后删除信息
                    _context.News.Remove(newsentity);

                    return await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                return 204;
            }
        }
    }
}
