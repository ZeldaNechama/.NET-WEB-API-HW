using Microsoft.AspNetCore.Mvc;
using tasks.Models;
using tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace tasks.Controllers;

[ApiController]
[Route("[controller]")]

public class MyTaskController : ControllerBase
{
    private IMyTasksServices myTaskService;
    private int User_Id;


    public MyTaskController(IMyTasksServices myTaskService, IHttpContextAccessor httpContextAccessor)
    {
        this.myTaskService = myTaskService;
        User_Id = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);

    }

   
    [Authorize(Policy = "User")]
    [HttpGet("All")] 
    public ActionResult<List<MyTask>> Get()
    {
        return myTaskService.GetAll();
    }

    [Authorize(Policy = "User")]
    [HttpGet] 
    public ActionResult<List<MyTask>> GetMyTasks()
    {
        return myTaskService.GetMyTasks(User_Id);
    }


    [Authorize(Policy = "User")]
    [HttpGet("{id}")]
    public ActionResult<MyTask> Get(int id)
    {
        var myTask = myTaskService.Get(id);
        if (myTask == null)
            return NotFound();
        return Ok(myTask);
    }

    [Authorize(Policy = "User")]
    [HttpPost]
    public IActionResult Post(MyTask newMyTask)
    {

        var newId = myTaskService.Post(newMyTask);
        return CreatedAtAction(nameof(Post), new { id = newId }, newMyTask);
    }

    [Authorize(Policy = "User")]
    [HttpPut("{id}")]
    public ActionResult Put(int id, MyTask newMyTask)
    {
        myTaskService.Put(id, newMyTask);
        return Ok();
    }

    [Authorize(Policy = "User")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        myTaskService.Delete(id);
        return Ok();
    }
}