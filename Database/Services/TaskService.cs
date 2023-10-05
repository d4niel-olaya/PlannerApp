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
    public async Task<Taskes> CreateTask(Taskes model)
    {
        var result = await _taskRepository.CreateAsync(model);
        return (Taskes)result.obj;
    }

    public async Task<List<Taskes>> GetTaskes()
    {
        var result = await _taskRepository.GetAsync();
        return (List<Taskes>)result.obj;
    }

    public void SetProjectId(int id)
    {
        _taskRepository.SetId(id);
    }

    public async Task<Taskes> UpdateTask(Taskes model)
    {
        var result = await _taskRepository.UpdateAsync(model);
        return (Taskes) result.obj;
    }
}


public interface ITaskService
{
     Task<Taskes> CreateTask(Taskes model);

     Task<List<Taskes>> GetTaskes();

    void SetProjectId(int id);

    Task<Taskes> UpdateTask(Taskes model);
}
