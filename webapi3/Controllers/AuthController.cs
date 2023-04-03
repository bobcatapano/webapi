using webapi3.Commons;
using webapi3.ViewModels;
using WebApi3_DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterVM registerVM)
        {
            var IsExist = await userManager.FindByNameAsync(registerVM.Name);
            return null;
        }

    }
} 