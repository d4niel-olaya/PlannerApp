using PlannerApp.Database.Models;
using PlannerApp.Database.Repository;


namespace PlannerApp.Database.Services;


public class TaskService : ITaskService
{


       private readonly TaskRepository _taskRepository;

     public TaskService(TaskRepository taskRepository)
     {
          _taskRepository = taskRepository;
     }
    public Task<Taskes> CreateTask(Taskes model)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Taskes>> GetTaskes()
    {
        return  await _taskRepository.GetAsync();
    }

    public void SetProjectId(int id)
    {
        _taskRepository.SetId(id);
    }
}


public interface ITaskService
{
     Task<Taskes> CreateTask(Taskes model);

     Task<List<Taskes>> GetTaskes();

    void SetProjectId(int id);
}
