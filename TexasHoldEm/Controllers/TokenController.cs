//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Reflection.Metadata;
//using TexasHoldEm.Models;
//using Microsoft.AspNetCore.Identity;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace TexasHoldEm.Controllers
//{
//    [Route("api/token")]
//    public class TokenController : Controller
//    {
//        private PokerManagerContext _context;
//        private UserManager<Players> _userManager;
//        private IConfiguration _config;

//        public TokenController(PokerManagerContext context,
//            UserManager<Players> userManager,
//            IConfiguration config)
//        {
//            _context = context;
//            _userManager = userManager;
//            _config = config;
//        }

//        [HttpPost("Auth")]
//        public async Task<IActionResult>
//            Jwt([FromBody]TokenRequestViewModel model)
//        {
//            if (model == null)
//                return new StatusCodeResult(500);

//            switch (model.grant_type)
//            {
//                case "password": return await GetToken(model);
//                default: return new UnauthorizedResult();
//            }
//        }

//        private async Task<IActionResult>
//            GetToken(TokenRequestViewModel model)
//        {
//            try
//            {
//                var user = await _userManager.FindByNameAsync(model.username);

//                if (user == null || !await _userManager.CheckPasswordAsync(user, model.password))
//                    return new UnauthorizedResult();

//                DateTime now = DateTime.UtcNow;

//                var claims = new[]
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
//                };


//                var tokenExpirationMins = _config.GetValue<int>
//                    ("Auth:Jwt:TokenExpirationInMinutes");
//                var issuerSigningKey = new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(_config["Auth:Jwt:Key"]));

//                var token = new JwtSecurityToken(
//                    issuer: _config["Auth:Jwt:Issuer"],
//                    audience: _config["Auth:Jwt:Audience"],
//                    claims: claims,
//                    notBefore: now,
//                    expires:
//                    now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
//                    signingCredentials: new SigningCredentials(issuerSigningKey,
//                    SecurityAlgorithms.HmacSha512)
//                    );

//                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
//                // build & return the response
//                var response = new TokenResponseViewModel()
//                {
//                    token = encodedToken,
//                    expiration = tokenExpirationMins
//                };
//                return Json(response);

//            }
//            catch (Exception ex)
//            {
//                return new UnauthorizedResult();
//            }
//        }
//    }
//}
