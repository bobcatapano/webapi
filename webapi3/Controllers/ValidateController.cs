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
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;

namespace webapi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        public ValidateController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Route("Validate")]
        [HttpGet]
        public async Task<IActionResult> Validate(string token, string username)
        {
            // int UserId = new UserRepository().username);
            int UserId = userManager.FindByNameAsync(username).Id;
            if (UserId == 0) return Ok(new Response
            {
                Status = "Invalid",
                Message = "Invalid User."
            });
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return Ok(new Response
                {
                    Status = "Success",
                    Message = "OK",
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Invalid",
                Message = "Invalid Token."
            });
        }
        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = TokenManager.GetPrincipal(token);
            if (principal == null) return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }
    }
}
