using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;
using Microsoft.AspNetCore.Authorization;
using Winner.Models.Response;
using System.Linq.Expressions;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsTypeController : ControllerBase
    {
        private readonly INewsTypeService _newstypeservice;
        public NewsTypeController(INewsTypeService newsTypeService)
        {
            _newstypeservice = newsTypeService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetOne(int id)
        {
            if (id > 0)
            {
                var responseModel = await _newstypeservice.GetOne(id);

                return new JsonResult(responseModel);
            }
            else 
            {
                return new JsonResult(new ResponseModel { code=0,result="参数错误"});
            } 
        }
        /// <summary>
        /// 根据条件获取前几条新闻分类
        /// </summary>
        /// <param name="head"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetList(bool head,int topCount)
        {
            var responseModel = await _newstypeservice.GetList(c=>c.ishead== head, topCount);

            return new JsonResult(responseModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetPageList(int pagesize, int pageindex, string keyword)
        {
            if (pagesize <= 0 || pageindex < 1)
            {
                return new JsonResult(new ResponsePageModel
                {
                    code = 0,
                    result = "参数错误",
                    total = 0
                });
            }
            List<Expression<Func<NewsType, bool>>> wheres = new List<Expression<Func<NewsType, bool>>>();
            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.typename.Contains(keyword));

            var responsePageModel = await _newstypeservice.GetList(pagesize, pageindex, wheres);

            return new JsonResult(responsePageModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Add([FromBody] NewsType newsType)
        {
            if (ModelState.IsValid)
            {
                var responseModel= await _newstypeservice.Add(newsType);
                return new JsonResult(responseModel);
            }
            else
            {
                return new JsonResult(new ResponseModel
                {

                    code = 400,
                    result = "参数验证失败"
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<JsonResult> Edit([FromBody] NewsType newsType)
        {
            if (ModelState.IsValid)
            {
                //数据库操作
                var responseModel = await _newstypeservice.EditOne(newsType);

                return new JsonResult(responseModel);
            }
            else
            {
                return new JsonResult(new ResponseModel
                {

                    code = 400,
                    result = "参数验证失败"
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return new JsonResult(new ResponseModel
                {
                    code = 400,
                    result = "参数错误"
                });
            }
            //数据库操作
            var responseModel = await _newstypeservice.DeleteOne(id);

            return new JsonResult(responseModel);
        }
    }
}
