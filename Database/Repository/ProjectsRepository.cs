using MySqlConnector;
using PlannerApp.Database;
using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;
using Microsoft.AspNetCore.Components;



namespace PlannerApp.Database.Repository;


public class ProjectsRepository : Repository<IDb, Project>
{
    private readonly IDb _dbService;

    //private  int _id;
    private readonly UserTemp _userTemp;

    private readonly UserLocalStorage _local;
    public ProjectsRepository(IDb db, UserTemp userTemp, UserLocalStorage local)
    {
        _dbService = db;   
        _userTemp = userTemp;
        _local = local;
        
    }

    public override async Task<Project> CreateAsync(Project model)
    {
        await _dbService.OpenDb();
        var id = await _local.GetId();
        using(var cmd = _dbService.GetCommand())
        {
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "INSERT INTO Projects(ProjectName, ProjectDescription, UserIdProject) VALUES(@N, @D, @U)";
            cmd.Parameters.AddWithValue("@N",model.ProjectName);
            cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
           cmd.Parameters.AddWithValue("@U",id);
            //cmd.Parameters.AddWithValue("@R",user.UserRole);
            await cmd.ExecuteNonQueryAsync();
            
        }
        await _dbService.CloseDb();
        return new Project{ ProjectName = model.ProjectName, ProjectDescription = model.ProjectDescription};

    }

    public override  async Task<List<Project>> GetAsync()
    {
        var projectList = new List<Project>();
        var id = await _local.GetId();
        await _dbService.OpenDb();
        using var cmd = _dbService.GetCommand();
        cmd.Connection = _dbService.GetProvider();
        cmd.CommandText = "SELECT ProjectId, ProjectName, ProjectDescription FROM Projects WHERE UserIdProject = @N";
        cmd.Parameters.AddWithValue("@N",id);
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
        await _dbService.CloseDb();
        return projectList;
    }



    public override Task<Project> UpdateAsync(Project model)
    {
        throw new NotImplementedException();
    }
}