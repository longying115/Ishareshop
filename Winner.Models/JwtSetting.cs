using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models
{
    public class JwtSetting
    {
        public string SecurityKey { get; set; }//密钥
        public string Issuer { get; set; }//颁发者
        public string Audience { get; set; }//接收者
        public double ExpireSeconds { get; set; }//过期时间

        //刷新Token接收者
        public string RefreshTokenAudience { get; set; }
        //刷新Token 过期时间
        public double RefreshTokenExpiresMinutes { get; set; }


    }
}
