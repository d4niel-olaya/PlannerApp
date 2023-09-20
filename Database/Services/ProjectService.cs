

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
        return await _projectRepository.CreateAsync(model);
    }

    public async  Task<List<Project>> GetProjects()
    {
        return await _projectRepository.GetAsync();
    }

}


public interface IProjectService
{
    Task<Project> CreateProject(Project model);
    Task<List<Project>> GetProjects();
}