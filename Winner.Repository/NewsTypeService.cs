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
        private readonly ILogger _logger;

        public NewsTypeService(AccountContext accountContext,ILogger<NewsTypeService> logger)
        {
            _context = accountContext;
            _logger = logger;
        }
        public async Task<int> AddAsync(NewsType newsType)
        {
            _context.NewsType.Add(newsType);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteOneAsync(NewsType newsType)
        {
            _context.NewsType.Remove(newsType);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteListAsync(List<NewsType> list)
        {
            _context.NewsType.RemoveRange(list);

            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> EditOneAsync(NewsType newsType)
        {
            _context.NewsType.Update(newsType);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<NewsType> GetOneAsync(int id)
        {
            NewsType newsType = await _context.NewsType.FindAsync(id);
            return newsType;
        }
        public async Task<List<NewsType>> GetListAsync(List<Expression<Func<NewsType, bool>>> wheres)
        {
            var list = _context.NewsType.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            var data = await list.ToListAsync();
            return data;
        }
        public async Task<List<NewsType>> GetListAsync(Expression<Func<NewsType, bool>> where, int topCount)
        {
            var list = _context.NewsType.Where(where);
            var newsTypeList = await list.OrderBy(s => s.Sort).Take(topCount).ToListAsync();
            return newsTypeList;
        }
        public async Task<int> GetCountAsync(List<Expression<Func<NewsType, bool>>> wheres)
        {
            var list = _context.NewsType.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = await list.CountAsync();
            return total;
        }
        public async Task<List<NewsType>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<NewsType, bool>>> wheres)
        {
            var list = _context.NewsType.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            //int total = list.Count();

            var pagedata = await list.OrderBy(s => s.Sort).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

            return pagedata;
        }
    }
}
