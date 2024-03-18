using tasks.Models;

namespace tasks.Interfaces;
 public interface IMyTasksServices
{
      List<MyTask> GetAll();
      MyTask Get(int id);
      int Post(MyTask newTask);      
      void Put(int id, MyTask newTask);
        
      void Delete(int id);
}