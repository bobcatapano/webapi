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
            if (IsExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }

            AppUser appUser = new AppUser
            {
                    UserName = registerVM.Name,
                    AccountType = registerVM.AccountTypes,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNo,
                    Password = registerVM.Password,
                    ShopName = registerVM.ShopName,
                    BusinessType = registerVM.BusinessType,
                    UserRole = registerVM.UserRole,
                    IsDeleted = registerVM.IsDeleted
            };

            var result = await userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded) {
               return StatusCode(StatusCodes.Status500InternalServerError, new Response
               {
                    Status = "Error",
                    Message = "User creation failed! Please check details and try again"
               });
            }

            if (!await roleManager.RoleExistsAsync(registerVM.UserRole))
            {
                await roleManager.CreateAsync(new IdentityRole(registerVM.UserRole));
            }

            if (await roleManager.RoleExistsAsync(registerVM.UserRole))
            {
               await userManager.AddToRoleAsync(appUser, registerVM.UserRole); 
            }

            return Ok(new Response
            {
               Status = "Success",
               Message = "User created Successfully!"
            });
        }
    }
} 