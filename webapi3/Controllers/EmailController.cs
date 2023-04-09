using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using webapi3.ViewModels;

namespace webapi3.Controllers
{

    [Authorize]
    public class EmailController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("Email")]
        [HttpPost]
        public async Task<IActionResult> Email([FromBody] EmailVM emailVM)
        {
            return Ok();
        }
    }
}
