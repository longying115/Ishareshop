using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;


namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductClassController : ControllerBase
    {
        private readonly IProductClassService _productclassservice;

        public ProductClassController(IProductClassService productclassservice)
        {
            _productclassservice = productclassservice;
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
        public async Task<IEnumerable<ProductClass>> GetList(int? pid, int? level, int? page, int? pagesize)
        {
            IEnumerable<ProductClass> productClasses = await _productclassservice.GetList(pid, level, page, pagesize);

            return productClasses;
        }
        /// <summary>
        /// 根据分类ID获取产品分类
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<ProductClass> Get(int id)
        {
            if (id > 0)
            {
                ProductClass productClass = await _productclassservice.Get(id);

                return productClass;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="productclass">分类内容属性</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductClass productclass)
        {
            if (ModelState.IsValid)
            {
                    int resval = await _productclassservice.Post(productclass);

                //判断、处理返回值
                if (resval == 1)
                {
                    return new JsonResult(new
                    {
                        code = 200,
                        status = "OK",
                        message = "添加成功"
                    });
                }
                else if (resval == 204)
                {
                    return new JsonResult(new
                    {
                        code = 204,
                        status = "NoContent",
                        message = "数据操作异常"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        code = resval,
                        status = "Unknown",
                        message = "添加失败"
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {

                    code = 400,
                    status = "BadRequest",
                    message = "ProdcutClass参数错误"
                });
            }
        }

       /// <summary>
       /// 修改产品分类
       /// </summary>
       /// <param name="id">分类ID</param>
       /// <param name="productclass">分类内容属性</param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductClass productclass)
        {
            if (id < 1)
            {
                return new JsonResult(new
                {
                    code = 400,
                    status = "BadRequest",
                    message = "id参数错误"
                });
            }
            if (ModelState.IsValid)
            {
                //数据库操作
                int resval = await _productclassservice.Put(id, productclass);
                //判断、处理返回值
                if (resval == 1)
                {
                    return new JsonResult(new
                    {
                        code = 200,
                        status = "OK",
                        message = "修改成功"
                    });
                }
                else if (resval == 204)
                {
                    return new JsonResult(new
                    {
                        code = 204,
                        status = "NoContent",
                        message = "数据操作异常"
                    });
                }
                else if (resval == 404)
                {
                    return new JsonResult(new
                    {
                        code = 404,
                        status = "NotFound",
                        message = "修改失败,记录不存在"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        code = resval,
                        status = "Unknown",
                        message = "修改失败"
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {

                    code = 400,
                    status = "BadRequest",
                    message = "ProdcutClass参数错误"
                });
            }
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return new JsonResult(new
                {
                    code = 400,
                    status = "BadRequest",
                    message = "id参数错误"
                });
            }
            //数据库操作
            int resval =await _productclassservice.Delete(id);
            //判断、处理返回值
            if (resval == 1)
            {
                return new JsonResult(new
                {
                    code = 200,
                    status="OK",
                    message = "删除成功"
                });
            }
            else if (resval == 204)
            {
                return new JsonResult(new
                {
                    code = 204,
                    status = "NoContent",
                    message = "数据操作异常"
                });
            }
            else if (resval == 404)
            {
                return new JsonResult(new
                {
                    code = 404,
                    status = "NotFound",
                    message = "删除失败,记录不存在"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    code = resval,
                    status = "Unknown",
                    message = "删除失败"
                });
            }
        }
    }
}
