using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winner.IRepository;
using Winner.Models;
using Winner.Models.Response;

namespace Winner.AdminSystem.Controllers
{
    public class NewsController : Controller
    {
        private INewsService _newsService;
        private IWebHostEnvironment _webHost;

        public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
        {
            _newsService = newsService;
            _webHost = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 如果要添加的图片放在第三方存储设备上，请给一个地址就可以了，
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Add([FromBody] News news, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                if (news.ClassId <= 0 || string.IsNullOrEmpty(news.Title) || string.IsNullOrEmpty(news.TextContent))
                {
                    return new JsonResult(new ResponseModel { code = 0, result = "参数有误" });
                }
                var files = collection.Files;
                if (files.Count > 0)
                {
                    string webRootPath = _webHost.WebRootPath;
                    string relativeDirPath = "\\NewsPicture";
                    string absolutePath = webRootPath + relativeDirPath;
                    string[] fileType = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                    string extension = Path.GetExtension(files[0].FileName);
                    if (fileType.Contains(extension))
                    {
                        if (Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                        var filePath = absolutePath + "\\" + fileName;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await files[0].CopyToAsync(stream);
                        }
                        news.SmallPicture = "/NewsPicture/" + fileName;
                    }
                    return new JsonResult(new ResponseModel { code = 0, result = "图片格式有误" });
                }
                //return new JsonResult(new ResponseModel { code = 0, result = "请上传新闻图片" });
                var responseModel = await _newsService.AddAsync(news);
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
        [HttpDelete("{id}", Name = "Delete")]
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
            //先删除图片
            var newsModel = await _newsService.GetOneAsync(id);
            if (newsModel != null)
            {
                var savePath = newsModel.SmallPicture;
                if (!string.IsNullOrEmpty(savePath))
                {
                    var realyPath = Path.Combine(_webHost.WebRootPath + savePath);

                    System.IO.File.Delete(realyPath);
                }
            }

            //数据库操作
            var responseModel = await _newsService.DeleteOneAsync(newsModel);

            return new JsonResult(responseModel);
        }
    }
}