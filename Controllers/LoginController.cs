using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using tasks.Services;


namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]
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

       
}





