using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Winner.Extends
{
    public static class ShopCart
    {
        #region 添加到购物车AddShoppingCar
        /// <summary>
        /// 添加到购物车AddShoppingCar
        /// </summary>
        /// <param name="num">数量 如果存在产品 负数是减少 
        /// 正数是增加 如果不存在 直接增加</param>
        /// <param name="id">产品ID</param>
        /// <param name="expires">cookies保存的天数</param>
        /// <remarks>这里的方法就是把在原有的Cookie基础上判断是否有
        /// 这个产品 如果有 在原有数量上增加 没有 就直接增加 如果是负
        /// 数 就是减少 如果负数的数量大于等于
        /// 原有数量 设置为0 对应后面的读出操作</remarks>
        public static void AddShoppingCar(string num, string id, int expires)
        {
            //if (System.Web.HttpContext.Current.Request.Cookies["Products"] != null)
            //{
            //    System.Web.HttpCookie cookie;
            //    string cookievalue = System.Web.HttpContext.Current.Request.Cookies["Products"].Value;
            //    if (System.Web.HttpContext.Current.Request.Cookies["Products"].Values[id.ToString()] == null)
            //    {
            //        cookievalue = cookievalue + "&" + id + "=" + num;//&2828=5
            //    }
            //    else
            //    {
            //        int num1 = int.Parse(System.Web.HttpContext.Current.Request.Cookies["Products"].Values[id.ToString()].ToString()) + int.Parse(num);
            //        if (num1 > 0)
            //        {
            //            System.Web.HttpContext.Current.Request.Cookies["Products"].Values[id.ToString()] = num1.ToString();
            //        }
            //        else
            //        {
            //            System.Web.HttpContext.Current.Request.Cookies["Products"].Values[id.ToString()] = "0";
            //        }
            //        cookievalue = System.Web.HttpContext.Current.Request.Cookies["Products"].Value;
            //    }
            //    cookie = new System.Web.HttpCookie("Products", cookievalue);
            //    if (expires != 0)
            //    {
            //        DateTime dt = DateTime.Now;
            //        TimeSpan ts = new TimeSpan(expires, 0, 0, 20);
            //        cookie.Expires = dt.Add(ts);
            //    }
            //    System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            //}
            //else
            //{
            //    System.Web.HttpCookie newcookie = new HttpCookie("Products");
            //    if (expires != 0)
            //    {
            //        DateTime dt = DateTime.Now;
            //        TimeSpan ts = new TimeSpan(expires, 0, 0, 20);
            //        newcookie.Expires = dt.Add(ts);
            //    }
            //    newcookie.Values[id.ToString()] = num;
            //    System.Web.HttpContext.Current.Response.AppendCookie(newcookie);
            //}
        }
        #endregion
    }
}
