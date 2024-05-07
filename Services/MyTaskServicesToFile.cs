using tasks.Interfaces;
using tasks.Models;
using System.Text.Json;

namespace tasks.Services;

public class MyTaskServicesToFile : IMyTasksServices
{


    private List<MyTask> mytasks { get; }
    private string filePath;

    public MyTaskServicesToFile(IWebHostEnvironment webHost)
    {
        this.filePath = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "tasks.json");
        using (var jsonFile = File.OpenText(filePath))
        {
            this.mytasks = JsonSerializer.Deserialize<List<MyTask>>(jsonFile.ReadToEnd(),
             new JsonSerializerOptions
             {
                 PropertyNameCaseInsensitive = true

             });

        }
    }

    private void SaveToFile()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(mytasks));
    }
    public List<MyTask> GetAll() => mytasks;


    public List<MyTask> GetMyTasks(int id)
    {
        return mytasks.FindAll(t => t.User_Id == id);
    }
    // public MyTask Get(int id)
    // {
    //     return mytasks.FirstOrDefault(t => t.User_Id == id);
    // }
    public MyTask Get(int id) => mytasks.FirstOrDefault(t => t.Id == id);

    public int Post(MyTask myTask)
    {
        myTask.Id = mytasks.Count() + 1;
        mytasks.Add(myTask);
        SaveToFile();
        return myTask.Id;
    }

    public void Delete(int id)
    {
        var myTask = Get(id);
        if (myTask is null)
            return;

        mytasks.Remove(myTask);
        SaveToFile();
    }

    public void Put(int id, MyTask myTask)
    {
        var index = mytasks.FindIndex(t => t.Id == myTask.Id);
        MyTask oldTask=mytasks.FirstOrDefault(t => t.Id == id);
        myTask.User_Id = oldTask.Id;
        if (index == -1)
            return;

        mytasks[index] = myTask;
        SaveToFile();
    }


    public int Count => mytasks.Count();
}
