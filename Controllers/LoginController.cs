using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using tasks.Services;


namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Policy = "User")]
public class LoginController : ControllerBase
{
    IUserService userService;
    public LoginController(IUserService userService)
    {
        this.userService = userService;
    }

    //option1
    [HttpPost]
    public ActionResult Login([FromBody] User user)
    {
        User current_user = userService.GetUser(user);
        if (current_user == null)
            return BadRequest();
        if (current_user.IsAdmin)
        {
            var claims = new List<Claim>
                {
                    new Claim("type","User"),
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

    // [HttpPost]
    // public ActionResult Login([FromBody] User user)
    // {
    //     // Implement proper authentication logic
    //     if (user.Name == "zn" && user.Password == "zndbvkwk@#vhvbj")
    //     {
    //         var claims = new List<Claim>
    //     {
    //         new Claim("type", "Admin"),
    //         new Claim("name", user.Name),
    //         new Claim("id", user.Id.ToString())
    //     };
    //         var token = LoginServices.GetToken(claims);
    //         return new OkObjectResult(LoginServices.WriteToken(token));
    //     }
    //     else
    //     {
    //        var claims = new List<Claim>
    //     {
    //         new Claim("type", "User"),
    //         new Claim("name", user.Name!),
    //         new Claim("id", user.Id.ToString())
    //     };
    //         var token = LoginServices.GetToken(claims);
    //         return new OkObjectResult(LoginServices.WriteToken(token));
    //     }
          
    // }















    
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





