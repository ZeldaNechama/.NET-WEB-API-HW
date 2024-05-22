using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService userService;
    private int User_Id;

    public UserController(IUserService userService,IHttpContextAccessor httpContextAccessor)
    {
        this.userService = userService;
        User_Id = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);

    }

    [Authorize(Policy = "Admin")]
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return userService.GetAll();
    }

    [Authorize(Policy = "User")]
    [HttpGet("GetUser")]
    public ActionResult<User> GetUser()
    {
        var user = userService.Get(User_Id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public IActionResult Post(User newUser)
    {
        var newId = userService.Post(newUser);
        return CreatedAtAction(nameof(Post), new { id = newId }, newUser);
    }
    
    [Authorize(Policy = "User")]
    [HttpPut("{id}")]
    public ActionResult Put(int id, User newUser)
    {
        userService.Put(id, newUser);
        return Ok();
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        userService.Delete(id);
        return Ok();
    }

}
public static partial class Utilities
{
    public static void AddUser(this IServiceCollection services)
    {
        services.AddSingleton<Interfaces.IUserService, Services.UserService>();
    }
}
