using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Ishareshop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductClassController : ControllerBase
    {
        private readonly AccountContext _context;
        private readonly ILogger<ProductController> _logger;
        private IHostingEnvironment _hostingEnvironment;

        public ProductClassController(AccountContext context, ILogger<ProductController> logger, IHostingEnvironment env)
        {
            _context = context;
            _logger = logger;
            _hostingEnvironment = env;
        }
        /// <summary>
        /// GET: api/ProductClass
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        //[HttpGet("{pid}",Name = "GetProductClassList")]
        public async Task<IEnumerable<ProductClass>> GetProductClassList(int? pid,int? level)
        {
            var productclassentiters = from s in _context.ProductClass.Where(s => s.isshow == true)
                              select s;

            if (pid != null)
            {
                productclassentiters =  _context.ProductClass.Where(s => s.pid == pid);
            }
            if (level != null)
            {
                productclassentiters = _context.ProductClass.Where(s => s.classlevel == level);
            }
            productclassentiters = productclassentiters.OrderBy(s => s.sort);

            IEnumerable<ProductClass> productclasslist =await productclassentiters.ToListAsync();

            return productclasslist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="level"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ProductClass>> GetProductClassList(int? pid,int? level,int? page,int? pagesize=8)
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
            if (page != null && page>0)
            {
                if (pagesize != null && pagesize <= 0)
                {
                    pagesize = 8;
                }

                productclassentiters = productclassentiters.Skip(Convert.ToInt32((page - 1) * pagesize)).Take(Convert.ToInt32(pagesize));
            }
            IList<ProductClass> productclasslist = await productclassentiters.ToListAsync();

            return productclasslist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetProductClass")]
        public async Task<ProductClass> GetProductClass(int id)
        {
            if (id > 0)
            {
                ProductClass productclass = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == id);

                return productclass;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// api/ProductClass
        /// </summary>
        /// <param name="productclass"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProductClassAdd([FromBody] ProductClass productclass)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductClass.Add(productclass);
                   int resval = await _context.SaveChangesAsync();

                    if (resval > 0)
                    {
                        return new JsonResult(new { 
                        
                            code=200,
                            message="添加成功"
                        });
                    }
                    else
                    {
                        return new JsonResult(new
                        {

                            code = 0,
                            message = "添加失败"
                        });
                    }
                }
                catch
                {
                    return new JsonResult(new
                    {

                        code = -1,
                        message = "数据操作异常"
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {

                    code = -2,
                    message = "获取参数错误"
                });
            }
        }
        /// <summary>
        /// PUT: api/ProductClass/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productclass"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdit(int id, [FromBody] ProductClass productclass)
        {
            if (id<1)
            {
                return new JsonResult(new { 
                    code=-3,
                    message="id参数错误"
                });
            }
            if (ModelState.IsValid)
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
                        int resval = await _context.SaveChangesAsync();

                        if (resval > 0)
                        {
                            return new JsonResult(new
                            {

                                code = 200,
                                message = "修改成功"
                            });
                        }
                        else
                        {
                            return new JsonResult(new
                            {

                                code = 0,
                                message = "修改失败"
                            });
                        }
                    }
                    else
                    {
                        return new JsonResult(new
                        {

                            code = -5,
                            message = "修改失败，记录不存在"
                        });
                    }
                    
                }
                catch
                {
                    return new JsonResult(new
                    {

                        code = -1,
                        message = "数据操作异常"
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {

                    code = -2,
                    message = "获取参数错误"
                });
            }
        }

        /// <summary>
        /// DELETE: api/ApiWithActions/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id<1)
            {
                return new JsonResult(new
                {
                    code = -3,
                    message = "id参数错误"
                });
            }
            try
            {
                var productclassentity = await _context.ProductClass.SingleOrDefaultAsync(s => s.id == id);

                if (productclassentity == null)
                {
                    return new JsonResult(new
                    {
                        code = -4,
                        message = "删除失败,记录不存在"
                    });
                }
                else
                {
                    //var savePath = banner.smallpicture;
                    //var realyPath = Path.Combine(_hostingEnvironment.WebRootPath + savePath);

                    //System.IO.File.Delete(realyPath);
                    //先删除图片，后删除信息
                    _context.ProductClass.Remove(productclassentity);

                   int resval=  await _context.SaveChangesAsync();

                    return new JsonResult(new
                    {
                        code = 200,
                        message = "删除成功"
                    });
                }

            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    code = -1,
                    message = "数据操作异常"
                });
            }
        }
    }
}
