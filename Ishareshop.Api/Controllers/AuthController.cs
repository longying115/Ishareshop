using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userservice;
        private readonly JwtSetting _jwtsetting;
        public AuthController(IUserService userservice, IOptions<JwtSetting> jwtsetting)
        {
            _userservice = userservice;
            _jwtsetting = jwtsetting.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> get()
        {
            return "Welcome";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authuser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] Winner.Models.ModelClass.MoAuthUser authuser)
        {
            var user = await _userservice.Login(authuser.UserName, authuser.PassWord);

            if (user == null)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "验证失败，账户或密码错误"
                });
            }


            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim("name", user.UserName),
                new Claim("role", user.Role),
                //new Claim("admin", user.IsAdmin.ToString(),ClaimValueTypes.Boolean)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //创建令牌
            var token = new JwtSecurityToken(
                    issuer: _jwtsetting.Issuer,
                    audience: _jwtsetting.Audience,
                    signingCredentials: creds,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddSeconds(_jwtsetting.ExpireSeconds)
                    );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JsonResult(new
            {
                Status = true,
                Token = jwtToken,
                Expires = DateTime.Now.AddSeconds(_jwtsetting.ExpireSeconds),
                Type = "Bearer"
            });
        }
    }
}