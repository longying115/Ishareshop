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
    public class ProductService : IProductService
    {
        private readonly AccountContext _context;
        private readonly ILogger _logger;

        public ProductService(AccountContext accountContext, ILogger<ProductService> logger)
        {
            _context = accountContext;
            _logger = logger;
        }
        public async Task<int> AddAsync(Products products)
        {
            _context.Products.Add(products);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteOneAsync(Products products)
        {
            _context.Products.Remove(products);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteListAsync(List<Products> list)
        {
            _context.Products.RemoveRange(list);

            int result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<int> EditOneAsync(Products products)
        {
            _context.Products.Update(products);
            return await _context.SaveChangesAsync();
            //try
            //{
            //    Products productsEntity = await _context.Products.SingleOrDefaultAsync(s => s.Id == id);
            //    if (productsEntity != null)
            //    {
            //        productsEntity.FistClassId = products.FistClassId;
            //        productsEntity.SecondClassId = products.SecondClassId;
            //        productsEntity.Sort = products.Sort;

            //        productsEntity.Title = products.Title;
            //        productsEntity.SmallTitle = products.SmallTitle;
            //        productsEntity.Remark = products.Remark;

            //        productsEntity.KeyTitle = products.KeyTitle;
            //        productsEntity.Keywords = products.Keywords;
            //        productsEntity.Description = products.Description;
            //        productsEntity.SmallPicture = products.SmallPicture;
            //        productsEntity.PictureTag = products.PictureTag;

            //        productsEntity.Sales = products.Sales;
            //        productsEntity.Score = products.Score;
            //        productsEntity.TextContent = products.TextContent;
            //        productsEntity.Parameter = products.Parameter;

            //        productsEntity.AddTime = products.AddTime;
            //        productsEntity.Hits = products.Hits;
            //        productsEntity.LastHitTime = products.LastHitTime;
            //        productsEntity.Praise = products.Praise;

            //        productsEntity.IsShow = products.IsShow;
            //        productsEntity.IsHome = products.IsHome;
            //        productsEntity.IsNew = products.IsNew;
            //        productsEntity.IsBest = products.IsBest;
            //        productsEntity.IsHot = products.IsHot;
            //        productsEntity.IsSale = products.IsSale;
            //        productsEntity.IsFocus = products.IsFocus;


            //        _context.Products.Update(productsEntity);
            //        return await _context.SaveChangesAsync();
            //    }
            //    else
            //    {
            //        return 404;
            //    }

            //}
            //catch
            //{
            //    return 204;
            //}
        }
        public async Task<Products> GetOneAsync(int id)
        {
            Products products = await _context.Products.SingleOrDefaultAsync(s => s.Id == id);

            return products;
        }
        public async Task<List<Products>> GetListAsync(List<Expression<Func<Products, bool>>> wheres)
        {
            var list = _context.Products.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            var data = await list.ToListAsync();
            return data;
        }
        public async Task<List<Products>> GetListAsync(Expression<Func<Products, bool>> where, int topCount)
        {
            var list = _context.Products.Where(where);
            var newslist = await list.OrderByDescending(s => s.AddTime).Take(topCount).ToListAsync();

            return newslist;
        }
        public async Task<int> GetCountAsync(List<Expression<Func<Products, bool>>> wheres)
        {
            var list = _context.Products.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = await list.CountAsync();
            return total;
        }
        public async Task<List<Products>> GetListAsync(int pageSize, int pageIndex, List<Expression<Func<Products, bool>>> wheres)
        {
            var list = _context.Products.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            var pageData = await list.OrderByDescending(s => s.AddTime).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

            return pageData;
        }

    }
}
