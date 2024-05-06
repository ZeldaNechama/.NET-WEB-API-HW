using tasks.Interfaces;
using tasks.Models;
using System.Text.Json;

namespace tasks.Services;

public class UserService : IUserService
{
    private List<User> users { get; }
    private string filePath;
    public UserService(IWebHostEnvironment webHost)
    {
        this.filePath = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "users.json");
        using (var jsonFile = File.OpenText(filePath))
        {
            this.users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
             new JsonSerializerOptions
             {
                 PropertyNameCaseInsensitive = true

             });

        }
    }

    private void SaveToFile()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(users));
    }
    public List<User> GetAll() => users;

    public User Get(int id) => users.FirstOrDefault(u => u.Id == id);

     public List<MyTask> GetTasks(int id) => users.Where(u => u.Id == id).SelectMany(u => u.TasksList).ToList();

     public User GetUser(User user ){
        return users.Find(u=>u.Id == user.Id&&u.Password == user.Password);
     }


    public int Post(User user)
    {
        user.Id = users.Count() + 1;
        users.Add(user);
        SaveToFile();
        return user.Id;
    }

    public void Delete(int id)
    {
        var user = Get(id);
        if (user is null)
            return;

        // UserService.Remove(user);
        users.Remove(user);
        SaveToFile();
    }

    public void Put(int id, User user)
    {
        var index = users.FindIndex(u => u.Id == user.Id);
        if (index == -1)
            return;

        users[index] = user;
        SaveToFile();
    }


    public int Count => users.Count();
}
