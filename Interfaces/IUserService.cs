using tasks.Models;

namespace tasks.Interfaces;
 public interface IUserService
{
      List<User> GetAll();
      User Get(int id);
      int Post(User user);      
      void Put(int id, User user);
        
      void Delete(int id);
}