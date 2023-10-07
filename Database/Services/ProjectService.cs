

using PlannerApp.Database.Models;
using PlannerApp.Database.Repository;

namespace PlannerApp.Database.Services;


public class ProjectService : IProjectService
{
    private readonly ProjectsRepository _projectRepository;

    public ProjectService(ProjectsRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<Project> CreateProject(Project model)
    {
        var result = await _projectRepository.CreateAsync(model);
        return (Project)result.obj;
    }

    public async  Task<List<Project>> GetProjects()
    {
       var result =  await _projectRepository.GetAsync();
       return (List<Project>)result.obj;
    }

    public async Task<Project> UpdateProject(Project model)
    {
        var result = await _projectRepository.UpdateAsync(model);
        return (Project)result.obj;
    }

}


public interface IProjectService
{
    Task<Project> CreateProject(Project model);
    Task<List<Project>> GetProjects();

    Task<Project> UpdateProject(Project model);
}