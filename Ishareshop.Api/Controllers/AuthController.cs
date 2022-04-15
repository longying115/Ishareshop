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
        private readonly IUserService _userService;
        private readonly JwtSetting _jwtSetting;
        public AuthController(IUserService userservice, IOptions<JwtSetting> jwtsetting)
        {
            _userService = userservice;
            _jwtSetting = jwtsetting.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> get()
        {
            await Task.Delay(2);
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
            var user = await _userService.Login(authuser.UserName, authuser.PassWord);

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

            //////////////////////
            claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString(),ClaimValueTypes.Integer32),//唯一标识
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Gender,"男"),
                };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //创建令牌
            var token = new JwtSecurityToken(
                    issuer: _jwtSetting.Issuer,
                    audience: _jwtSetting.Audience,
                    signingCredentials: creds,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
                    );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JsonResult(new
            {
                Status = true,
                Token = jwtToken,
                Expires = DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds),
                Type = "Bearer"
            });
        }
        /// <summary>
        /// JWT的认证有两种：
        /// 第一种：结合redis,把生成的token放入redis中设置过期时间（短于token的过期时间），如果redis过期则重新获取access_token
        /// 第二种：新写一个JWT服务，用RefreshTokenAudience认证，多加一个 RefreshToken过期时间很长,通过这个token获取新的access_token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<JsonResult> CreatedTokenAsync(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresDatetime = DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds);

            var token = new JwtSecurityToken(issuer: _jwtSetting.Issuer, audience: _jwtSetting.Audience, claims: claims, notBefore: DateTime.Now, expires: expiresDatetime, signingCredentials
                : creds);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JsonResult(new
            {
                Status = true,
                Token = jwtToken,
                Expires = expiresDatetime,
                Type = "Bearer"
            });
        }

    }
}