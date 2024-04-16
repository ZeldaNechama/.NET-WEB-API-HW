using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy ="User")]
public class LoginController : ControllerBase
{
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = "ClearanceLevel1")]
        public ActionResult<String> AccessPublicFiles()
        {
            return new OkObjectResult("Public Files Accessed");
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = "ClearanceLevel2")]
        public ActionResult<String> AccessClassifiedFiles()
        {
            return new OkObjectResult("Classified Files Accessed");
        }

}
