using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Winner.Models;

using Winner.IRepository;
using Winner.Models.Response;
using System.Linq.Expressions;

namespace Winner.Repository
{
    public class BannerService:IBannerService
    {
        private readonly AccountContext _context;

        public BannerService(AccountContext accountContext)
        {
            _context = accountContext;
        }
        public async Task<ResponseModel> Add(Banner banner)
        {
            _context.Banner.Add(banner);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "广告图添加成功" };
            return new ResponseModel { code = 0, result = "广告图添加失败" };
        }
        public async Task<ResponseModel> GetOne(int id)
        {
            Banner banner = await _context.Banner.FindAsync(id);
            if (banner == null)
                return new ResponseModel { code = 0, result = "广告图不存在" };
            return new ResponseModel { code = 200, result = "广告图获取成功", data = banner };
        }
        public async Task<ResponseModel> GetList(Expression<Func<Banner, bool>> where, int topCount)
        {
            var list = _context.Banner.Where(where);
            var topdata = await list.OrderBy(s => s.Sort).Take(topCount).ToListAsync();

            var response = new ResponseModel
            {
                code = 200,
                result = "广告图获取成功"
            };
            return new ResponseModel { code = 200, result = "广告图获取成功", data = topdata };
        }
        public async Task<ResponsePageModel> GetList(int pagesize, int pageindex, List<Expression<Func<Banner, bool>>> wheres)
        {
            var list = _context.Banner.Where(s => true);
            foreach (var item in wheres)
            {
                list = list.Where(item);
            }
            int total = list.Count();

            var pagedata = await list.OrderBy(s => s.Sort).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToListAsync();

            return new ResponsePageModel { code = 200, result = "分页广告图获取成功", total = total, data = pagedata };
        }
        public async Task<ResponseModel> EditOne(Banner banner)
        {
            Banner bannerEntity = await _context.Banner.FirstOrDefaultAsync(s => s.Id == banner.Id);
            if (bannerEntity == null)
                return new ResponseModel { code = 0, result = "广告图不存在" };
            bannerEntity.Sort = banner.Sort;
            bannerEntity.BannerName = banner.BannerName;
            bannerEntity.Picture = banner.Picture;
            bannerEntity.BackgroundImg = banner.BackgroundImg;
            bannerEntity.LinkUrl = banner.LinkUrl;
            bannerEntity.ColumnArea = banner.ColumnArea;

            bannerEntity.IsShow = banner.IsShow;
            bannerEntity.IsMobile = banner.IsMobile;

            bannerEntity.GMTCreate = banner.GMTCreate;
            bannerEntity.CreateAdminId = banner.CreateAdminId;
            bannerEntity.GMTModified = banner.GMTModified;

            bannerEntity.ModifiedAdminId = banner.ModifiedAdminId;
            bannerEntity.ModifiedIp = banner.ModifiedIp;

            _context.Banner.Update(bannerEntity);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
                return new ResponseModel { code = 200, result = "广告图修改成功" };
            return new ResponseModel { code = 0, result = "广告图修改失败" };
        }

        public async Task<ResponseModel> DeleteOne(int id)
        {
            //这里只用try-catch就可以了。
            //如果是同时有几个操作数据库的步骤。为了保持数据的完整性，用一个事务括起来。

            try
            {
                Banner banner = await _context.Banner.FindAsync(id);
                if (banner == null)
                    return new ResponseModel { code = 0, result = "广告图不存在" };

                //var savePath = banner.smallpicture;
                //if (!string.IsNullOrEmpty(savePath))
                //{
                //    var realyPath = Path.Combine(_hostingenvironment.WebRootPath + savePath);

                //    File.Delete(realyPath);
                //}
                //先删除图片，后删除信息
                _context.Banner.Remove(banner);
                int i = await _context.SaveChangesAsync();
                if (i > 0)
                    return new ResponseModel { code = 200, result = "广告图删除成功" };
                return new ResponseModel { code = 0, result = "广告图删除失败" };

            }
            catch (Exception e)
            {
                return new ResponseModel { code = 400, result = e.Message };
            }
        }
    }
}
