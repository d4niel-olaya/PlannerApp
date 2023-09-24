using PlannerApp.Database.Models;
using PlannerApp.Database.Repository;


namespace PlannerApp.Database.Services;


public class TaskService 
{

}


public interface ITaskService
{
     Task<Taskes> CreateTask(Taskes model);

     Task<List<Taskes>> GetTaskes();

}
