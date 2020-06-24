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
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsservice;
        private IWebHostEnvironment _webhost;
        public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
        {
            _newsservice = newsService;
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
                var responseModel = await _newsservice.GetOne(id);

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
        /// <param name="head"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetList(bool head, int topCount)
        {
            var responseModel = await _newsservice.GetList(c => c.ishead == head, topCount);

            return new JsonResult(responseModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="typeid"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetPageList(int pagesize, int pageindex,int typeid, string keyword)
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
            List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();

            if (typeid > 0)
                wheres.Add(s => s.tid == typeid);

            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.title.Contains(keyword));
            

            var responsePageModel = await _newsservice.GetList(pagesize, pageindex, wheres);

            return new JsonResult(responsePageModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Add([FromBody] News news)
        {
            if (ModelState.IsValid)
            {
                if (news.tid<=0 || string.IsNullOrEmpty(news.title) || string.IsNullOrEmpty(news.textcontent))
                {
                    return new JsonResult(new ResponseModel { code=0,result="参数有误" });
                }
                //var files = collection.Files;
                //if (files.Count>0)
                //{
                //    string webRootPath = _webhost.WebRootPath;
                //    string relativeDirPath = "\\NewsPicture";
                //    string absolutePath = webRootPath + relativeDirPath;
                //    string[] fileType = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                //    string extension = Path.GetExtension(files[0].FileName);
                //    if (fileType.Contains(extension))
                //    {
                //        if (Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);
                //        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                //        var filePath = absolutePath + "\\" + fileName;
                //        using (var stream = new FileStream(filePath, FileMode.Create))
                //        {
                //            await files[0].CopyToAsync(stream);
                //        }
                //        news.smallpicture = "/NewsPicture/" + fileName;
                //    }
                //    return new JsonResult(new ResponseModel { code = 0, result = "图片格式有误" });
                //}
                //return new JsonResult(new ResponseModel { code = 0, result = "请上传新闻图片" });
                var responseModel = await _newsservice.Add(news);
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
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<JsonResult> Edit([FromBody] News news)
        {
            if (ModelState.IsValid)
            {
                //数据库操作
                var responseModel = await _newsservice.EditOne(news);

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
            //var newsModel = await _newsservice.GetOne(id);
            //if (newsModel.code == 200)
            //{
            //    var savePath = newsModel.data.smallpicture;
            //    if (!string.IsNullOrEmpty(savePath))
            //    {
            //        var realyPath = Path.Combine(_webhost.WebRootPath + savePath);

            //        System.IO.File.Delete(realyPath);
            //    }
            //}

            //数据库操作
            var responseModel = await _newsservice.DeleteOne(id);

            //return new JsonResult(responseModel);
            return Ok(responseModel);
        }

        
    }
}
