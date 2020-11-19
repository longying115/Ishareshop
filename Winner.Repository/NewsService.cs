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
    public class NewsService : INewsService
    {
        private readonly AccountContext _context;
        private readonly ILogger _logger;

        public NewsService(AccountContext accountContext,ILogger<NewsService> logger)
        {
            _context = accountContext;
            _logger = logger;
        }
        public async Task<int> AddAsync(News news)
        {
            _context.News.Add(news);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteOneAsync(News news)
        {
            _context.News.Remove(news);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteListAsync(List<News> list)
        {
            _context.News.RemoveRange(list);

            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> EditOneAsync(News news)
        {
            _context.News.Update(news);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<News> GetOneAsync(string id)
        {
            News news = await _context.News.FindAsync(id);
            return news;
        }
        public async Task<List<News>> GetListAsync(List<Expression<Func<News,bool>>> wheres)
        {
            var list = _context.News.Where(s=>true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            var data =await list.ToListAsync();
            return data;
        }
        public async Task<List<News>> GetListAsync(Expression<Func<News, bool>> where, int topCount)
        {
            var list = _context.News.Where(where);
            var newsList = await list.OrderByDescending(s => s.GMTCreate).Take(topCount).ToListAsync();

            return newsList;
        }
        public async Task<int> GetCountAsync(List<Expression<Func<News, bool>>> wheres)
        {
            var list = _context.News.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = await list.CountAsync();
            return total;
        }
        public async Task<List<News>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<News, bool>>> wheres)
        {
            var list = _context.News.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            //int total = list.Count();

            var pageData = await list.OrderByDescending(s => s.GMTCreate).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

            return pageData;
        }

    }
}
