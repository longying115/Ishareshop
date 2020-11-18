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
using Serilog;
using Serilog.AspNetCore;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using Winner.Models.Request.Commands;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;
        private IWebHostEnvironment _webHost;
        private readonly ILogger _logger;
        public ProductController(IProductService productservice, IWebHostEnvironment webHostEnvironment, ILogger logger)
        {
            _productservice = productservice;
            _webHost = webHostEnvironment;
            _logger = logger;
        }
        /// <summary>
        /// 获取单条产品详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetOne(int id)
        {
            _logger.Information("开始记录日志");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Delay(1000);
            sw.Reset();
            sw.Stop();

            if (id > 0)
            {
                var news = await _productservice.GetOneAsync(id);

                if (news == null)
                    return new ResponseModel { code = 0, result = "产品不存在" };
                return new ResponseModel { code = 200, result = "产品获取成功", data = news };
            }
            else
            {
                return new ResponseModel { code = 0, result = "参数错误" };
            }

        }
        /// <summary>
        /// 根据条件获取产品
        /// </summary>
        /// <param name="isHome"></param>
        /// <param name="isShow"></param>
        /// <param name="fistClassId"></param>
        /// <param name="secondClassId"></param>
        /// <returns></returns>
        public async Task<ResponseModel> GetList(bool isHome, bool isShow, int fistClassId = 0, int secondClassId = 0)
        {
            List<Expression<Func<Products, bool>>> wheres = new List<Expression<Func<Products, bool>>>();
            if (fistClassId > 0)
            {
                wheres.Add(s => s.FistClassId == fistClassId);
            }
            if (secondClassId > 0)
            {
                wheres.Add(s => s.SecondClassId == secondClassId);
            }
            wheres.Add(s => s.IsShow == isShow);
            wheres.Add(s => s.IsHome == isHome);

            var List = await _productservice.GetListAsync(wheres);
            return new ResponseModel { code = 200, result = "产品列表获取成功", data = List };
        }
        /// <summary>
        /// 获取推荐产品前几条
        /// </summary>
        /// <param name="isHome"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetList(bool isHome, int topCount)
        {
            var productsList = await _productservice.GetListAsync(c => c.IsHome == isHome, topCount);
            return new ResponseModel { code = 200, result = "产品获取成功", data = productsList };
        }
        /// <summary>
        /// 分页获取产品列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fistClassId"></param>
        /// <param name="secondClassId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponsePageModel> GetPageList(int pageSize, int pageIndex, string keyword, int fistClassId = 0, int secondClassId = 0)
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
            List<Expression<Func<Products, bool>>> wheres = new List<Expression<Func<Products, bool>>>();

            if (fistClassId > 0)
                wheres.Add(s => s.FistClassId == fistClassId);
            if (secondClassId > 0)
            {
                wheres.Add(s => s.SecondClassId == secondClassId);
            }

            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.Title.Contains(keyword));

            int total = await _productservice.GetCountAsync(wheres);

            var pageData = await _productservice.GetListAsync(pageSize, pageIndex, wheres);

            return new ResponsePageModel { code = 200, result = "分页产品获取成功", total = total, data = pageData };
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> Add([FromBody] Products product)
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
                var result = await _productservice.AddAsync(product);
                if (result > 0)
                    return new ResponseModel { code = 200, result = "产品添加成功" };
                return new ResponseModel { code = 0, result = "产品添加失败" };
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
        /// 编辑产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ResponseModel> Edit([FromBody] ProductSave product)
        {
            if (ModelState.IsValid)
            {
                //这里只用try-catch就可以了。
                //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。

                var productEntity = await _productservice.GetOneAsync(product.Id);
                if (productEntity == null)
                    return new ResponseModel { code = 0, result = "产品不存在" };

                productEntity.FistClassId = product.FistClassId;
                productEntity.SecondClassId = product.SecondClassId;
                productEntity.Sort = product.Sort;
                productEntity.Title = product.Title;
                productEntity.SmallTitle = product.SmallTitle;
                productEntity.Remark = product.Remark;
                productEntity.KeyTitle = product.KeyTitle;
                productEntity.Keywords = product.Keywords;
                productEntity.Description = product.Description;

                productEntity.SmallPicture = product.SmallPicture;
                productEntity.PictureTag = product.PictureTag;
                productEntity.Sales = product.Sales;
                productEntity.Score = product.Score;
                productEntity.TextContent = product.TextContent;
                productEntity.Parameter = product.Parameter;
                productEntity.AddTime = product.AddTime;
                productEntity.Hits = product.Hits;
                productEntity.LastHitTime = product.LastHitTime;
                productEntity.Praise = product.Praise;

                productEntity.IsShow = product.IsShow;
                productEntity.IsHome = product.IsHome;
                productEntity.IsNew = product.IsNew;
                productEntity.IsBest = product.IsBest;
                productEntity.IsHot = product.IsHot;
                productEntity.IsSale = product.IsSale;
                productEntity.IsFocus = product.IsFocus;

                //数据库操作
                var result = await _productservice.EditOneAsync(productEntity);

                if (result > 0)
                    return new ResponseModel { code = 200, result = "产品修改成功" };
                return new ResponseModel { code = 0, result = "产品修改失败" };
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
        /// 删除单条产品
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
            var product = await _productservice.GetOneAsync(id);
            if (product == null)
            {
                return new ResponseModel
                {
                    code = 404,
                    result = "产品不存在"
                };
            }
            //先删除图片，后删除信息,文件的操作放到控制器上，方便第三方存储
            var savePath = product.SmallPicture;
            if (!string.IsNullOrWhiteSpace(savePath))
            {
                var realyPath = Path.Combine(_webHost.WebRootPath + savePath);

                System.IO.File.Delete(realyPath);
            }

            //数据库操作
            var result = await _productservice.DeleteOneAsync(product);

            if (result > 0)
                return new ResponseModel { code = 200, result = "产品删除成功" };
            return new ResponseModel { code = 0, result = "产品删除失败" };
        }
        /// <summary>
        /// 批量删除产品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ResponseModel> DeleteMany(int[] ids)
        {
            try
            {
                List<Expression<Func<Products, bool>>> wheres = new List<Expression<Func<Products, bool>>>();

                wheres.Add(s => ids.Contains(s.Id));

                var list = await _productservice.GetListAsync(wheres);
                if (list.Count > 0 && list.Any())
                {
                    int i = await _productservice.DeleteListAsync(list);

                    if (i > 0)
                        return new ResponseModel { code = 200, result = "批量产品删除成功" };
                    return new ResponseModel { code = 0, result = "批量产品删除失败" };
                }
                return new ResponseModel { code = 0, result = "删除的产品不存在" };
            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }
    }
}
