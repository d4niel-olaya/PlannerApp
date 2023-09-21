using PlannerApp.Database;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;


namespace PlannerApp.Database.Repository;


public class ProjectsRepository : Repository<IDb, Project>
{
    private readonly IDb _dbService;

    private readonly UserTemp _userTemp;
    public ProjectsRepository(IDb db, UserTemp userTemp)
    {
        _dbService = db;   
        _userTemp = userTemp;
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

    public override  async Task<List<Project>> GetAsync()
    {
        var projectList = new List<Project>();
        await _dbService.OpenDb();
        using var cmd = _dbService.GetCommand();
        cmd.Connection = _dbService.GetProvider();
        cmd.CommandText = "SELECT ProjectId, ProjectName, ProjectDescription FROM Projects WHERE UserIdProject = @N";
        cmd.Parameters.AddWithValue("@N", _userTemp.GetCurrentUserId());
        using var reader = await cmd.ExecuteReaderAsync();
         while(await reader.ReadAsync())
         {
            var project = new Project
            {
                ProjectId = reader.GetInt32(0),
                ProjectName = reader.GetString(1),
                ProjectDescription = reader.GetString(2)
            };
            projectList.Add(project);
        }
        return projectList;
    }

    public override Task<Project> UpdateAsync(Project model)
    {
        throw new NotImplementedException();
    }
}