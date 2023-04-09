using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace webapi3.Controllers
{
    public class EmailController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        [Route("Email")]
        [HttpGet]
        public async Task<IActionResult> Email()
        {
            return Ok();
        }
    }
}
