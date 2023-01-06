using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Monsta_backend.DBContexts;
using Monsta_backend.JwtFeatures;
using Monsta_backend.Models;
using Org.BouncyCastle.Crypto.Generators;

namespace Monsta_backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly MyDBContext db;
        private readonly JwtHandler _jwtHandler;
        public AuthController(JwtHandler jwtHandler, MyDBContext myDBContext)
        {
            db = myDBContext;
            _jwtHandler = jwtHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = db.Users.FirstOrDefault(user => user.Email == userForAuthentication.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userForAuthentication.Password, user.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Wrong username or password" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}
