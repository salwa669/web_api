using Ecommerce.DTO;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser>userManger, IConfiguration Configuration)
        {
           userManager = userManger;
           configuration = Configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save data base
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDto.UserName;
            userModel.Email = registerDto.Email;
            userModel.PhoneNumber = registerDto.Phone;
            userModel.Address = registerDto.Address;
            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok("Add Success");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check identity and  "Create Token"
            ApplicationUser userModel = await userManager.FindByNameAsync(loginDto.UserName);
            if (userModel != null)
            {
                if (await userManager.CheckPasswordAsync(userModel, loginDto.Password) == true)
                {
                    var mytoken = await GenerateToke(userModel);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
                else
                {
                    return BadRequest("User Name and Password Not Valid");
                   // return Unauthorized();//401
                }
            }
            return Unauthorized();
        }
           
        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("intakeNo", "42"));//Custom
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Jti "Identifier Token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //---------------------------------(: Token :)--------------------------------------
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var token = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
