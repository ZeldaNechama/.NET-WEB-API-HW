using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using tasks.Services;


namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "User")]
public class LoginController : ControllerBase
{
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "ClearanceLevel1")]
    //Admin& User  can get taskslist
    public ActionResult<String> AccessPublicFiles()
    {
        return new OkObjectResult("Public Files Accessed");
    }

//only Admin can acccess this;
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "ClearanceLevel2")]
    public ActionResult<String> AccessClassifiedFiles()
    {
        return new OkObjectResult("Classified Files Accessed");
    }
    //login for both wuth changes for each one;
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        //User?.FindFirst
        var dt = DateTime.Now;

        if (User.IsAdmin == false
        || User.Password != $"W{dt.Year}#{dt.Day}!")
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

        var token = LoginServices.GetToken(claims);

        return new OkObjectResult(LoginServices.WriteToken(token));
    }


    [HttpPost]
    [Route("[action]")]
    [Authorize(Policy = "Admin")]
    public IActionResult GenerateBadge([FromBody] User User)
    {
        var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("ClearanceLevel", User.IsAdmin.ToString()),
            };

        var token = LoginServices.GetToken(claims);

        return new OkObjectResult(LoginServices.WriteToken(token));
    }
}


        


