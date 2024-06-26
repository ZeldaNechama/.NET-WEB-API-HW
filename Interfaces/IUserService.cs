using tasks.Models;

namespace tasks.Interfaces;
 public interface IUserService
{
      List<User> GetAll();
     // List<MyTask> GetTasks(int id);
      User GetUser(User user);
      User Get(int user_id);
      int Post(User user);      
      void Put(int id, User user);
        
      void Delete(int id);
}