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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsTypeController : ControllerBase
    {
        private readonly INewsTypeService _newsTypeService;
        private IWebHostEnvironment _webHost;
        public NewsTypeController(INewsTypeService newsTypeService, IWebHostEnvironment webHostEnvironment)
        {
            _newsTypeService = newsTypeService;
            _webHost = webHostEnvironment;
        }

        /// <summary>
        /// 获取新闻分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetOne(int id)
        {
            if (id > 0)
            {
                var newsType = await _newsTypeService.GetOneAsync(id);

                if (newsType == null)
                    return new ResponseModel { code = 0, result = "新闻分类不存在" };
                return new ResponseModel { code = 200, result = "新闻分类获取成功", data = newsType };
            }
            else
            {
                return new ResponseModel { code = 0, result = "参数错误" };
            }
        }
        /// <summary>
        /// 根据条件获取前几条新闻分类
        /// </summary>
        /// <param name="isHead"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetList(bool isHead, int topCount)
        {
            var newsTypeList = await _newsTypeService.GetListAsync(c => c.IsHead == isHead, topCount);

            return new ResponseModel { code = 200, result = "新闻分类获取成功", data = newsTypeList };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponsePageModel> GetPageList(int pageSize, int pageIndex, string keyword)
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
            List<Expression<Func<NewsType, bool>>> wheres = new List<Expression<Func<NewsType, bool>>>();
            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.TypeName.Contains(keyword));

            int total = await _newsTypeService.GetCountAsync(wheres);

            var pageData = await _newsTypeService.GetListAsync(pageSize, pageIndex, wheres);

            return new ResponsePageModel { code = 200, result = "分页新闻分类获取成功", total = total, data = pageData };
            //return new JsonResult(responsePageModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> Add([FromBody] NewsType newsType)
        {
            if (ModelState.IsValid)
            {
                var result = await _newsTypeService.AddAsync(newsType);
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
        /// 
        /// </summary>
        /// <param name="newsType"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ResponseModel> Edit([FromBody] NewsType newsType)
        {
            if (ModelState.IsValid)
            {
                var newsTypeEntity = await _newsTypeService.GetOneAsync(newsType.Id);
                if (newsTypeEntity == null)
                    return new ResponseModel { code = 0, result = "新闻分类不存在" };

                newsTypeEntity.Sort = newsType.Sort;
                newsTypeEntity.TypeName = newsType.TypeName;
                newsTypeEntity.KeyTitle = newsType.KeyTitle;
                newsTypeEntity.Keywords = newsType.Keywords;
                newsTypeEntity.Description = newsType.Description;
                newsTypeEntity.Picture = newsType.Picture;
                newsTypeEntity.PictureTag = newsType.PictureTag;
                newsTypeEntity.GMTCreate = newsType.GMTCreate;
                newsTypeEntity.GMTLastHit = newsType.GMTLastHit;
                newsTypeEntity.IsShow = newsType.IsShow;
                newsTypeEntity.IsHead = newsType.IsHead;

                //数据库操作
                var result = await _newsTypeService.EditOneAsync(newsTypeEntity);

                if (result > 0)
                    return new ResponseModel { code = 200, result = "新闻分类修改成功" };
                return new ResponseModel { code = 0, result = "新闻分类修改失败" };
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
        /// 删除单条新闻分类
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
            var newsType = await _newsTypeService.GetOneAsync(id);
            if (newsType == null)
            {
                return new ResponseModel
                {
                    code = 404,
                    result = "新闻分类不存在"
                };
            }
            //先删除图片，后删除信息,文件的操作放到控制器上，方便第三方存储
            var savePath = newsType.Picture;
            if (!string.IsNullOrWhiteSpace(savePath))
            {
                var realyPath = Path.Combine(_webHost.WebRootPath + savePath);

                System.IO.File.Delete(realyPath);
            }

            //数据库操作
            var result = await _newsTypeService.DeleteOneAsync(newsType);

            if (result > 0)
                return new ResponseModel { code = 200, result = "新闻分类删除成功" };
            return new ResponseModel { code = 0, result = "新闻分类删除失败" };
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
                List<Expression<Func<NewsType, bool>>> wheres = new List<Expression<Func<NewsType, bool>>>();

                wheres.Add(s => ids.Contains(s.Id));

                var list = await _newsTypeService.GetListAsync(wheres);
                if (list.Count > 0 && list.Any())
                {
                    int i = await _newsTypeService.DeleteListAsync(list);

                    if (i > 0)
                        return new ResponseModel { code = 200, result = "批量新闻分类删除成功" };
                    return new ResponseModel { code = 0, result = "批量新闻分类删除失败" };
                }
                return new ResponseModel { code = 0, result = "删除的新闻分类不存在" };
            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }

    }
}
