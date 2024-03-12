using Microsoft.AspNetCore.Mvc;
using lesson2.Models;

namespace lesson2.Controllers;


[ApiController]
[Route("[controller]")]
public class MyTasksController : ControllerBase
{
    private List<MyTask> tasks;

    public MyTasksController()
    {
        tasks = new List<MyTask>
        {
            new MyTask{Id =1,Name ="Do HW",IsDone =false},
            new MyTask{Id=2,Name="Go to the store",IsDone=true},
            new MyTask{Id=3,Name="Call X",IsDone=true}
        };
    }

    [HttpGet]
    public IEnumerable<MyTask> Get()
    {
        return tasks;

    }

    [HttpGet("{id}")]
    public MyTask Get(int id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }


    [HttpPost]
    public int Post(MyTask newTask)
    {
        int max = tasks.Max(t => t.Id);
        newTask.Id = max + 1;
        tasks.Add(newTask);
        return newTask.Id;
    }

    [HttpPut("{id}")]
    public void Put(int id, MyTask newTask)
    {
        if (id == newTask.Id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                int index = tasks.IndexOf(task);
                tasks[index] = newTask;
            }
        }
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {

        var task = tasks.Find(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
        }

    }
}
