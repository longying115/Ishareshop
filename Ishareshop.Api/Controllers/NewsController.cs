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
using System.IO;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private IWebHostEnvironment _webHost;
        public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
        {
            _newsService = newsService;
            _webHost = webHostEnvironment;
        }
        /// <summary>
        /// 获取单条新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetOne(int id)
        {
            if (id > 0)
            {
                var news = await _newsService.GetOneAsync(id);

                if (news == null)
                    return new ResponseModel { code = 0, result = "新闻不存在" };
                return new ResponseModel { code = 200, result = "新闻获取成功", data = news };
            }
            else
            {
                return new ResponseModel { code = 0, result = "参数错误" };
            }
        }
        /// <summary>
        /// 根据条件获取新闻
        /// </summary>
        /// <param name="isHead"></param>
        /// <param name="isShow"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public async Task<ResponseModel> GetList(bool isHead, bool isShow, int classId)
        {
            List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();
            if (classId > 0)
            {
                wheres.Add(s => s.ClassId == classId);
            }
            wheres.Add(s => s.IsShow == isShow);
            wheres.Add(s => s.IsHead == isHead);

            var List = await _newsService.GetListAsync(wheres);
            return new ResponseModel { code = 200, result = "新闻列表获取成功", data = List };
        }
        /// <summary>
        /// 获取推荐新闻前几条
        /// </summary>
        /// <param name="isHead"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetList(bool isHead, int topCount)
        {
            var newsList = await _newsService.GetListAsync(c => c.IsHead == isHead, topCount);
            //if (newslist.Count()>0 && newslist.Any())
            //{
                
            //}
            return new ResponseModel { code = 200, result = "新闻获取成功", data = newsList };
            //var response = new ResponseModel
            //{
            //    code = 200,
            //    result = "新闻获取成功"
            //};
            //foreach (var news in topdata)
            //{
            //    response.data.Add(new News
            //    {
            //        id = news.id,
            //        tid = news.tid,
            //        title = news.title,
            //        keytitle = news.keytitle,
            //        keywords = news.keywords,
            //        description = news.description,
            //        author=news.author,
            //        source=news.source,
            //        smallpicture=news.smallpicture,
            //        picturetag=news.picturetag,
            //        addtime=news.addtime,
            //        hits=news.hits,
            //        lasthittime=news.lasthittime,

            //        isshow = news.isshow,
            //        ishead = news.ishead
            //    });
            //}
            //return response;//也可以尝试一下下面这种方式是否可以
            //return new ResponseModel { code = 200, result = "新闻获取成功", data = topdata };
        }
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="classId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponsePageModel> GetPageList(int pageSize, int pageIndex,int classId, string keyword)
        {
            if (pageSize <= 0 || pageIndex < 1)
            {
                return new ResponsePageModel
                {
                    code = 0,
                    result = "参数错误",
                    total = 0
                };
            }
            List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();

            if (classId > 0)
                wheres.Add(s => s.ClassId == classId);

            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.Title.Contains(keyword));

            int total = await _newsService.GetCountAsync(wheres);

            var pageData = await _newsService.GetListAsync(pageSize, pageIndex, wheres);

            return new ResponsePageModel { code = 200, result = "分页新闻获取成功", total = total, data = pageData };
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> Add([FromBody] News news)
        {
            if (ModelState.IsValid)
            {
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
                var result = await _newsService.AddAsync(news);
                if (result > 0)
                    return new ResponseModel { code = 200, result = "新闻添加成功" };
                return new ResponseModel { code = 0, result = "新闻添加失败" };
            }
            else
            {
                string errorMsg = "参数验证失败";
                if (ModelState.ErrorCount > 0)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        errorMsg += ModelState.GetValidationState(key) + "|";
                    }
                    errorMsg.TrimEnd('|');
                }
                return new ResponseModel
                {

                    code = 400,
                    result = errorMsg
                };
            }
        }
        /// <summary>
        /// 编辑新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ResponseModel> Edit([FromBody] News news)
        {
            if (ModelState.IsValid)
            {
                //这里只用try-catch就可以了。
                //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。

                var newsEntity = await _newsService.GetOneAsync(news.Id);
                if (newsEntity == null)
                    return new ResponseModel { code = 0, result = "新闻不存在" };

                newsEntity.ClassId = news.ClassId;
                newsEntity.Title = news.Title;
                newsEntity.KeyTitle = news.KeyTitle;
                newsEntity.Keywords = news.Keywords;
                newsEntity.Description = news.Description;
                newsEntity.Author = news.Author;
                newsEntity.Source = news.Source;
                newsEntity.SmallPicture = news.SmallPicture;
                newsEntity.PictureTag = news.PictureTag;
                newsEntity.AddTime = news.AddTime;
                newsEntity.Hits = news.Hits;
                newsEntity.LastHitTime = news.LastHitTime;
                newsEntity.Praise = news.Praise;
                newsEntity.TextContent = news.TextContent;
                newsEntity.IsShow = news.IsShow;
                newsEntity.IsHome = news.IsHome;
                newsEntity.IsHead = news.IsHead;

                //数据库操作
                var result = await _newsService.EditOneAsync(newsEntity);

                if (result > 0)
                    return new ResponseModel { code = 200, result = "新闻修改成功" };
                return new ResponseModel { code = 0, result = "新闻修改失败" };
            }
            else
            {
                string errorMsg = "参数验证失败";
                if (ModelState.ErrorCount > 0)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        errorMsg += ModelState.GetValidationState(key) + "|";
                    }
                    errorMsg.TrimEnd('|');
                }
                return new ResponseModel
                {

                    code = 400,
                    result = errorMsg
                };
            }
        }
        /// <summary>
        /// 删除单条新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<ResponseModel> Delete(int id)
        {
            if (id < 1)
            {
                return new ResponseModel
                {
                    code = 400,
                    result = "参数错误"
                };
            }
            var news = await _newsService.GetOneAsync(id);
            if (news==null)
            {
                return new ResponseModel
                {
                    code = 404,
                    result = "新闻不存在"
                };
            }
            //先删除图片，后删除信息,文件的操作放到控制器上，方便第三方存储
            var savePath = news.SmallPicture;
            if (!string.IsNullOrWhiteSpace(savePath))
            {
                var realyPath = Path.Combine(_webHost.WebRootPath + savePath);

                System.IO.File.Delete(realyPath);
            }

            //数据库操作
            var result = await _newsService.DeleteOneAsync(news);

            if (result > 0)
                return new ResponseModel { code = 200, result = "新闻删除成功" };
            return new ResponseModel { code = 0, result = "新闻删除失败" };
        }
        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ResponseModel> DeleteMany(int[] ids)
        {
            try
            {
                List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();

                wheres.Add(s => ids.Contains(s.Id));

                var list = await _newsService.GetListAsync(wheres);
                if (list.Count > 0 && list.Any())
                {
                    int i = await  _newsService.DeleteListAsync(list);

                    if (i > 0)
                        return new ResponseModel { code = 200, result = "批量新闻删除成功" };
                    return new ResponseModel { code = 0, result = "批量新闻删除失败" };
                }
                return new ResponseModel { code = 0, result = "删除的新闻不存在" };
            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }
        
    }
}
