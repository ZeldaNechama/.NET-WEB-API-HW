using System.Text.Json;
using tasks.Interfaces;
using tasks.Models;


namespace tasks.Services;

public class MyTaskServicesToFile:IMyTasksServices
{

        private  List<MyTask>? mytasks { get; }
        private string filePath;
        public MyTaskServicesToFile(IWebHostEnvironment webHost)
        {
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "MyTask.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                mytasks = JsonSerializer.Deserialize<List<MyTask>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(mytasks));
        }
        public List<MyTask> GetAll() => mytasks;

        public MyTask Get(int id) => mytasks.FirstOrDefault(t => t.Id == id);

        public int Post(MyTask myTask)
        {
            myTask.Id = mytasks.Count() + 1;
            mytasks.Add(myTask);
            saveToFile();
            return myTask.Id;
        }

        public void Delete(int id)
        {
            var myTask = Get(id);
            if (myTask is null)
                return;

            mytasks.Remove(myTask);
            saveToFile();
        }

        public void Put(int id,MyTask myTask)
        {
            var index = mytasks.FindIndex(t => t.Id == myTask.Id);
            if (index == -1)
                return;

            mytasks[index] = myTask;
            saveToFile();
        }


    public int Count => mytasks.Count();
    }
