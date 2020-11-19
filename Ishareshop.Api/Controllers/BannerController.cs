using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;
using Microsoft.AspNetCore.Authorization;
using Winner.Models.Response;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerservice;
        private IWebHostEnvironment _webhost;
        public BannerController(IBannerService bannerService, IWebHostEnvironment webHostEnvironment)
        {
            _bannerservice = bannerService;
            _webhost = webHostEnvironment;
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
                var responseModel = await _bannerservice.GetOne(id);

                return new JsonResult(responseModel);
            }
            else
            {
                return new JsonResult(new ResponseModel { code = 0, result = "参数错误" });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetList(int area, int topCount)
        {
            var responseModel = await _bannerservice.GetList(c => c.ColumnArea == area, topCount);

            return new JsonResult(responseModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="area"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetPageList(int pagesize, int pageindex, int area, string keyword)
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
            List<Expression<Func<Banner, bool>>> wheres = new List<Expression<Func<Banner, bool>>>();

            if (area > 0)
                wheres.Add(s => s.ColumnArea == area);

            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.BannerName.Contains(keyword));

            
            var responsePageModel = await _bannerservice.GetList(pagesize, pageindex, wheres);

            return new JsonResult(responsePageModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Add([FromBody] Banner banner)
        {
            if (ModelState.IsValid)
            {
                var responseModel = await _bannerservice.Add(banner);
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
        /// <param name="banner"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<JsonResult> Edit([FromBody] Banner banner)
        {
            if (ModelState.IsValid)
            {
                //数据库操作
                var responseModel = await _bannerservice.EditOne(banner);

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
            ////先删除图片
            //var bannerModel = await _bannerservice.GetOne(id);
            //if (bannerModel.code == 200)
            //{
            //    var savePath = bannerModel.data.smallpicture;
            //    if (!string.IsNullOrEmpty(savePath))
            //    {
            //        var realyPath = Path.Combine(_webhost.WebRootPath + savePath);

            //        System.IO.File.Delete(realyPath);
            //    }
            //}

            //数据库操作
            var responseModel = await _bannerservice.DeleteOne(id);

            return new JsonResult(responseModel);
        }
    }
}
