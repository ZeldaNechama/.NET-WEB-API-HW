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
    IUserService userService;
    public LoginController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public ActionResult Login([FromBody] User user)
    {
        User current_user = userService.Get(user.Id);
        if (current_user == null)
            return BadRequest();
        if (current_user.IsAdmin)
        {
            var claims = new List<Claim>
                {
                    new Claim("type","Admin"),
                    new Claim("name",current_user.Name!),
                    new Claim("id",current_user.Id.ToString())
                };
            var token = LoginServices.GetToken(claims);
            return new OkObjectResult(LoginServices.WriteToken(token));
        }
        else{
             var claims = new List<Claim>
                {
                    new Claim("type","User"),
                    new Claim("name",current_user.Name!),
                    new Claim("id",current_user.Id.ToString())
                };
            var token = LoginServices.GetToken(claims);
            return new OkObjectResult(LoginServices.WriteToken(token));

        }
    }


    //     [HttpGet]
    //     [Route("[action]")]
    //     [Authorize(Policy = "ClearanceLevel1")]
    //     //Admin& User  can get taskslist
    //     public ActionResult<String> AccessPublicFiles()
    //     {
    //         return new OkObjectResult("Public Files Accessed");
    //     }

    // //only Admin can acccess this;
    //     [HttpGet]
    //     [Route("[action]")]
    //     [Authorize(Policy = "ClearanceLevel2")]
    //     public ActionResult<String> AccessClassifiedFiles()
    //     {
    //         return new OkObjectResult("Classified Files Accessed");
    //     }
    //     //login for both with changes for each one;
    //     [HttpPost]
    //     [Route("[action]")]
    //     public ActionResult<String> Login([FromBody] User User)
    //     {
    //         //User?.FindFirst
    //         var dt = DateTime.Now;
    // // User.IsAdmin == false
    //         if (User.Name!="zn"
    //         || User.Password != $"W{dt.Year}#{dt.Day}!")
    //         {
    //             return Unauthorized();
    //         }

    //         var claims = new List<Claim>
    //             {
    //                 // new Claim("IsAdmin", "true"),
    //                 new Claim("Name", "zn"),
    //                 new Claim("Password","zndbvkwk@#vhvbj")
    //             };

    //         var token = LoginServices.GetToken(claims);

    //         return new OkObjectResult(LoginServices.WriteToken(token));
    //     }


    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] User User)
    //     {
    //         var claims = new List<Claim>
    //             {
    //                 new Claim("type", "User"),
    //                 new Claim("ClearanceLevel", User.IsAdmin.ToString()),
    //             };

    //         var token = LoginServices.GetToken(claims);

    //         return new OkObjectResult(LoginServices.WriteToken(token));
    //     }
}





