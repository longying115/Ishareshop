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
using System.Linq.Expressions;

namespace Winner.Repository
{
    public class ProductClassService : IProductClassService
    {

        private readonly AccountContext _context;
        private readonly ILogger _logger;
        public ProductClassService(AccountContext accountcontext, ILogger<ProductClassService> logger)
        {
            _context = accountcontext;
            _logger = logger;
        }
        public async Task<int> AddAsync(ProductClass productClass)
        {
            _context.ProductClass.Add(productClass);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteOneAsync(ProductClass productClass)
        {
            _context.ProductClass.Remove(productClass);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteListAsync(List<ProductClass> list)
        {
            _context.ProductClass.RemoveRange(list);

            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> EditOneAsync(ProductClass productClass)
        {
            _context.ProductClass.Update(productClass);
            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<ProductClass> GetOneAsync(int id)
        {
            ProductClass newsType = await _context.ProductClass.FindAsync(id);
            return newsType;
        }
        public async Task<List<ProductClass>> GetListAsync(List<Expression<Func<ProductClass, bool>>> wheres)
        {
            var list = _context.ProductClass.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            var data = await list.ToListAsync();
            return data;
        }
        public async Task<List<ProductClass>> GetListAsync(Expression<Func<ProductClass, bool>> where, int topCount)
        {
            var list = _context.ProductClass.Where(where);
            var productClassList = await list.OrderBy(s => s.Sort).Take(topCount).ToListAsync();
            return productClassList;
        }
        public async Task<int> GetCountAsync(List<Expression<Func<ProductClass, bool>>> wheres)
        {
            var list = _context.ProductClass.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = await list.CountAsync();
            return total;
        }
        public async Task<List<ProductClass>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<ProductClass, bool>>> wheres)
        {
            var list = _context.ProductClass.Where(s => true);
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
