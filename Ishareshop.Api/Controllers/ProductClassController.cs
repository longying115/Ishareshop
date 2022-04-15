using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Winner.Models.Response;
using System.Linq.Expressions;
using System.IO;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductClassController : ControllerBase
    {
        private readonly IProductClassService _productClassService;
        private IWebHostEnvironment _webHost;
        public ProductClassController(IProductClassService productClassService, IWebHostEnvironment webHostEnvironment)
        {
            _productClassService = productClassService;
            _webHost = webHostEnvironment;
        }
        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetOne(int id)
        {
            if (id > 0)
            {
                var productClass = await _productClassService.GetOneAsync(id);

                if (productClass == null)
                    return new ResponseModel { code = 0, result = "产品分类不存在" };
                return new ResponseModel { code = 200, result = "产品分类获取成功", data = productClass };
            }
            else
            {
                return new ResponseModel { code = 0, result = "参数错误" };
            }
        }
        /// <summary>
        /// 根据条件获取前几条产品分类
        /// </summary>
        /// <param name="isHead"></param>
        /// <param name="topCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseModel> GetList(bool isHead, int topCount)
        {
            var productClassList = await _productClassService.GetListAsync(c => c.IsHead == isHead, topCount);

            return new ResponseModel { code = 200, result = "产品分类获取成功", data = productClassList };
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
            List<Expression<Func<ProductClass, bool>>> wheres = new List<Expression<Func<ProductClass, bool>>>();
            if (!string.IsNullOrEmpty(keyword))
                wheres.Add(s => s.ClassName.Contains(keyword));

            int total = await _productClassService.GetCountAsync(wheres);

            var pageData = await _productClassService.GetListAsync(pageSize, pageIndex, wheres);

            return new ResponsePageModel { code = 200, result = "分页产品分类获取成功", total = total, data = pageData };
            //return new JsonResult(responsePageModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productClass"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> Add([FromBody] ProductClass productClass)
        {
            if (ModelState.IsValid)
            {
                var result = await _productClassService.AddAsync(productClass);
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
        /// 
        /// </summary>
        /// <param name="productClass"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ResponseModel> Edit([FromBody] ProductClass productClass)
        {
            if (ModelState.IsValid)
            {
                var productClassEntity = await _productClassService.GetOneAsync(productClass.Id);
                if (productClassEntity == null)
                    return new ResponseModel { code = 0, result = "产品分类不存在" };

                productClassEntity.ParentId = productClass.ParentId;
                productClassEntity.ClassLevel = productClass.ClassLevel;
                productClassEntity.Sort = productClass.Sort;
                productClassEntity.ClassName = productClass.ClassName;
                productClassEntity.ClassRemark = productClass.ClassRemark;
                productClassEntity.KeyTitle = productClass.KeyTitle;
                productClassEntity.Keywords = productClass.Keywords;
                productClassEntity.Description = productClass.Description;
                productClassEntity.Picture = productClass.Picture;
                productClassEntity.PictureTag = productClass.PictureTag;
                productClassEntity.GMTCreate = productClass.GMTCreate;
                productClassEntity.GMTModified = productClass.GMTModified;
                productClassEntity.GMTLastHit = productClass.GMTLastHit;
                productClassEntity.IsShow = productClass.IsShow;
                productClassEntity.IsHead = productClass.IsHead;

                //数据库操作
                var result = await _productClassService.EditOneAsync(productClassEntity);

                if (result > 0)
                    return new ResponseModel { code = 200, result = "产品分类修改成功" };
                return new ResponseModel { code = 0, result = "产品分类修改失败" };
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
        /// 删除单条产品分类
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
            var productClass = await _productClassService.GetOneAsync(id);
            if (productClass == null)
            {
                return new ResponseModel
                {
                    code = 404,
                    result = "产品分类不存在"
                };
            }
            //先删除图片，后删除信息,文件的操作放到控制器上，方便第三方存储
            var savePath = productClass.Picture;
            if (!string.IsNullOrWhiteSpace(savePath))
            {
                var realyPath = Path.Combine(_webHost.WebRootPath + savePath);

                System.IO.File.Delete(realyPath);
            }

            //数据库操作
            var result = await _productClassService.DeleteOneAsync(productClass);

            if (result > 0)
                return new ResponseModel { code = 200, result = "产品分类删除成功" };
            return new ResponseModel { code = 0, result = "产品分类删除失败" };
        }
        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<ResponseModel> DeleteMany(int[] ids)
        {
            try
            {
                List<Expression<Func<ProductClass, bool>>> wheres = new List<Expression<Func<ProductClass, bool>>>();

                wheres.Add(s => ids.Contains(s.Id));

                var list = await _productClassService.GetListAsync(wheres);
                if (list.Count > 0 && list.Any())
                {
                    int i = await _productClassService.DeleteListAsync(list);

                    if (i > 0)
                        return new ResponseModel { code = 200, result = "批量产品分类删除成功" };
                    return new ResponseModel { code = 0, result = "批量产品分类删除失败" };
                }
                return new ResponseModel { code = 0, result = "删除的产品分类不存在" };
            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }
    }
}
