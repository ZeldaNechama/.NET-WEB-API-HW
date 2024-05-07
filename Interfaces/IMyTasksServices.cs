using tasks.Models;

namespace tasks.Interfaces;
public interface IMyTasksServices
{
      List<MyTask> GetAll();
      List<MyTask> GetMyTasks(int user_id);
      MyTask Get(int id);
      int Post(MyTask newTask);
      void Put(int id, MyTask newTask);

      void Delete(int id);
}