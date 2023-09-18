using PlannerApp.Database;
using PlannerApp.Database.Models;


namespace PlannerApp.Database.Repository;


public class ProjectsRepository : Repository<IDb, Project>
{
    public override Task<Project> CreateAsync(Project model)
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<Project>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<Project> UpdateAsync(Project model)
    {
        throw new NotImplementedException();
    }
}