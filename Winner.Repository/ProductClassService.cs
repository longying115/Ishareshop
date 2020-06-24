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
    public class ProductClassService:IProductClassService
    {

        private readonly AccountContext _context;
        private readonly ILogger _logger;
        private IHostingEnvironment _hostingenvironment;

        public ProductClassService(AccountContext accountcontext)
        {
            _context = accountcontext;
        }
        public async Task<IEnumerable<ProductClass>> GetList(int? pid, int? level, int? page, int? pagesize)
        {
            var productclassentiters = from s in _context.ProductClass.Where(s => s.isshow == true)
                                       select s;

            if (pid != null)
            {
                productclassentiters = productclassentiters.Where(s => s.pid == pid);

            }
            if (level != null)
            {
                productclassentiters = productclassentiters.Where(s => s.classlevel == level);
            }
            //排序
            productclassentiters = productclassentiters.OrderBy(s => s.sort);

            //分页
            if (page != null && page > 0)
            {
                if (pagesize == null || pagesize <= 0)
                {
                    pagesize = 8;
                }

                productclassentiters = productclassentiters.Skip(Convert.ToInt32((page - 1) * pagesize)).Take(Convert.ToInt32(pagesize));
            }
            IEnumerable<ProductClass> productclasslist = await productclassentiters.ToListAsync();

            return productclasslist;
        }
        public async Task<ProductClass> Get(int id)
        {
            ProductClass productclass = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == id);

            return productclass;
        }
        public async Task<int> Post(ProductClass productclass)
        {
            try
            {
                _context.ProductClass.Add(productclass);

                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 204;
            }
        }
        public async Task<int> Put(int id, ProductClass productclass)
        {
            try
            {
                ProductClass productclassentity = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == id);
                if (productclassentity != null)
                {
                    productclassentity.pid = productclass.pid;
                    productclassentity.classlevel = productclass.classlevel;
                    productclassentity.sort = productclass.sort;
                    productclassentity.classname = productclass.classname;
                    productclassentity.classremark = productclass.classremark;
                    productclassentity.keytitle = productclass.keytitle;
                    productclassentity.keywords = productclass.keywords;
                    productclassentity.description = productclass.description;
                    productclassentity.smallpicture = productclass.smallpicture;
                    productclassentity.isshow = productclass.isshow;

                    //productclass.id = id;

                    _context.ProductClass.Update(productclassentity);
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
                var productclassentity = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == id);

                if (productclassentity == null)
                {
                    return 404;
                }
                else
                {
                    var savePath = productclassentity.smallpicture;
                    if (!string.IsNullOrEmpty(savePath))
                    {
                        var realyPath = Path.Combine(_hostingenvironment.WebRootPath + savePath);

                        File.Delete(realyPath);
                    }
                    
                    //先删除图片，后删除信息
                    _context.ProductClass.Remove(productclassentity);

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
