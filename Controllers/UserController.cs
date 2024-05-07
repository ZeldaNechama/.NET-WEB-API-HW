// using Microsoft.AspNetCore.Mvc;
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

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [Authorize(Policy = "Admin")]
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return userService.GetAll();
    }

    // [Authorize(Policy = "User")]
    // [HttpGet("Get List Tasks")]
    // public ActionResult<List<MyTask>> GetTasks(int id)
    // {
    //     return userService.GetTasks(id);
    // }

    [Authorize(Policy = "User")]
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var user = userService.Get(id);
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
