using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Winner.Models;

using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json;


using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Ishareshop.Controllers
{
    /// <summary>
    /// 产品控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AccountContext _context;
        private readonly ILogger<ProductController> _logger;
        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        public ProductController(AccountContext context, ILogger<ProductController> logger, IHostingEnvironment env)
        {
            _context = context;
            _logger = logger;
            _hostingEnvironment = env;
        }
        // GET: api/Product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet("{id}",Name = "GetClass")]
        //public ProductClass GetClass(int id)
        //{
        //    ProductClass pcclass = _context.ProductClass.SingleOrDefault(s => s.id == id);

        //    return pcclass;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}",Name = "GetProductClass")]
        //public async Task<IList<ProductClass>> GetProductClass(int? id)
        //{
        //    IList<ProductClass> pclist = await _context.ProductClass.Where(s => s.isshow == true && s.id== id).ToListAsync();

        //    return pclist;
        //}
        /// <summary>
        /// 获取单个产品信息
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns>返回单个产品实体</returns>
        [HttpGet("{id}",Name = "GetProduct")]
        public async Task<Products> GetProduct(int? id)
        
        {
            if (id==null)
            {
                return null;
            }
            else
            {
                using (var transaction=_context.Database.BeginTransaction())
                {
                    try
                    {
                        var product = await _context.Products.SingleOrDefaultAsync(s => s.id == id);

                        if (product == null)
                        {
                            return null;
                        }
                        else
                        {
                            transaction.Commit();
                            return product;
                        }
                    }
                    catch (Exception e)
                    {
                        //这里关于异常日志怎么处理，有待学习
                        _logger.LogInformation("");
                        _logger.LogWarning(e.Message);
                        _logger.LogCritical("");
                        _logger.LogError("");

                        transaction.Rollback();
                        return null;

                    }
                }
            }
            
            
        }
        /// <summary>
        /// 根据产品分类ID获取一个产品列表
        /// </summary>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="cid">产品分类ID</param>
        /// <param name="count">需要条数</param>
        /// <returns>产品实体列表</returns>
        [HttpGet("{sortOrder}",Name = "GetProductList")]
        public async Task<IList<Products>> GetProductList(string sortOrder,int? cid,int count)
        {
            if (cid == null)
            {
                return null;
            }
            else
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        IList<Products> productlist = await _context.Products.Include(s => s.ProductClass).Where(s => s.classid == cid && s.isshow == true).Take(9).OrderBy(s => s.sort).AsNoTracking().ToListAsync();
                        transaction.Commit();
                        return productlist;
                        
                    }
                    catch (Exception e)
                    {
                        //这里关于异常日志怎么处理，有待学习
                        _logger.LogInformation("");
                        _logger.LogWarning(e.Message);
                        _logger.LogCritical("");
                        _logger.LogError("");

                        transaction.Rollback();
                        return null;

                    }
                }
            }
        }
        /// <summary>
        /// 根据条件获取产品列表并分页
        /// </summary>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="cid">产品分类ID</param>
        /// <param name="page">页码</param>
        /// <param name="pagesize">每页个数</param>
        /// <returns>产品列表</returns>
        [HttpGet]
        public async Task<IList<Products>> GetProductList(string sortOrder,int? cid,int? page,int? pagesize=8)
        {
            var productentiters = from s in _context.Products.Include(s => s.ProductClass).Where(s => s.isshow == true)
                              select s;

            if (cid != null)
            {
                var productclass = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == cid);
                if (productclass != null)
                {
                    if (productclass.classlevel == 1)
                    {
                        var secondclasslist = _context.ProductClass.Where(s => s.pid == cid).AsNoTracking().ToList();

                        if (secondclasslist.Count > 0)
                        {
                            int[] ClassArray = new int[secondclasslist.Count];

                            for (int i = 0; i < secondclasslist.Count; i++)
                            {
                                ClassArray[i] = secondclasslist[i].id;
                            }
                            productentiters = productentiters.Where(s => ClassArray.Contains(s.classid));
                        }
                        else
                        {
                            productentiters = productentiters.Where(s => s.classid == cid);
                        }
                    }
                    else if (productclass.classlevel==2)
                    {
                        productentiters = productentiters.Where(s => s.classid == cid);
                    }
                    else
                    {
                        productentiters = productentiters.Where(s => s.classid == cid);
                    }
                }
            }
            //排序
            switch (sortOrder)
            {
                case "sort":
                    productentiters = productentiters.OrderBy(s => s.sort);
                    break;
                case "sort_desc":
                    productentiters = productentiters.OrderByDescending(s => s.sort);
                    break;
                case "date_desc":
                    productentiters = productentiters.OrderByDescending(s => s.addtime);
                    break;
                case "date":
                    productentiters = productentiters.OrderBy(s => s.addtime);
                    break;
                case "sale_desc":
                    productentiters = productentiters.OrderByDescending(s => s.sales);
                    break;
                case "sale":
                    productentiters = productentiters.OrderBy(s => s.sales);
                    break;
                case "price_desc":
                    productentiters = productentiters.OrderByDescending(s => s.price);
                    break;
                case "price":
                    productentiters = productentiters.OrderBy(s => s.price);
                    break;
                default:
                    productentiters = productentiters.OrderBy(s => s.id);
                    break;
            }
            //分页做
            if (page != null)
            {
                productentiters = productentiters.Skip(Convert.ToInt32((page - 1) * pagesize)).Take(Convert.ToInt32(pagesize));
            }
            IList<Products> productslist =await productentiters.ToListAsync();

            return productslist;
        }
        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/Product/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// 删除单个产品
        /// </summary>
        /// <param name="id">产品ID</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
        /// <summary>
        /// 删除单条产品
        /// </summary>
        /// <param name="id">产品编号</param>
        /// <returns>返回json提示</returns>
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSingle(int? id)
        {
            if (id == null)
            {
                return new JsonResult(new
                {
                    Code=0,
                    Message="删除失败，参数不正确"
                });
            }
            else
            {

            }
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync();
                if (product == null)
                {
                    return new JsonResult(new
                    {
                        Code = 0,
                        Message = "删除失败，记录不存在"
                    });
                    //JsonSerializerOptions options = new JsonSerializerOptions();
                    //string jsonstring = JsonSerializer.Serialize(objectoption, options);
                }
                else
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();

                    return new JsonResult(new
                    {
                        Code = 200,
                        Message = "删除成功"
                    });
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    Code = 0,
                    Message = e.Message
                });
            }

        }
    }
}
