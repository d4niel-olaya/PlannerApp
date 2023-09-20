using PlannerApp.Database;
using PlannerApp.Database.Models;


namespace PlannerApp.Database.Repository;


public class ProjectsRepository : Repository<IDb, Project>
{
    private readonly IDb _dbService;


    public ProjectsRepository(IDb db)
    {
        _dbService = db;
    }

    public override async Task<Project> CreateAsync(Project model)
    {
        await _dbService.OpenDb();
        using(var cmd = _dbService.GetCommand())
        {
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "INSERT INTO Projects(ProjectName, ProjectDescription) VALUES(@N, @D)";
            cmd.Parameters.AddWithValue("@N",model.ProjectName);
            cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
           // cmd.Parameters.AddWithValue("@P",user.UserPassword);
            //cmd.Parameters.AddWithValue("@R",user.UserRole);
            await cmd.ExecuteNonQueryAsync();
            
        }
        await _dbService.CloseDb();
        return new Project{ ProjectName = model.ProjectName, ProjectDescription = model.ProjectDescription};

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