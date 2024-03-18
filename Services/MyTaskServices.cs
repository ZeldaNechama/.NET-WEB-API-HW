using tasks.Models;
using tasks.Interfaces;

namespace tasks.Services;

public class MyTaskService : IMyTasksServices
{
    private List<MyTask> tasksList;
    public MyTaskService()
    {
        tasksList = new List<MyTask>
        {
            new MyTask{Id =1,Name ="Do HW",IsDone =false},
            new MyTask{Id=2,Name="Go to the store",IsDone=true},
            new MyTask{Id=3,Name="Call X",IsDone=true},
        };
    }

    public List<MyTask> GetAll() => tasksList;

    public MyTask Get(int id)
    {
      return tasksList.FirstOrDefault(t => t.Id == id);
    }
    public int Post(MyTask newTask)
    {
        int max = tasksList.Max(t => t.Id);
        newTask.Id = max + 1;
        tasksList.Add(newTask);
        return newTask.Id;
    }
    public void Put(int id, MyTask newTask)
    {
        if (id == newTask.Id)
        {
            var myTask = tasksList.Find(t => t.Id == id);
            if (myTask != null)
            {
                int index = tasksList.IndexOf(myTask);
                tasksList[index] = newTask;
            }
        }
    }

    public void Delete(int id)
    {

        var myTask = tasksList.Find(t => t.Id == id);
        if (myTask != null)
        {
            tasksList.Remove(myTask);
        }

    }
}