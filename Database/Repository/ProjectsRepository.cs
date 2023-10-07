using MySqlConnector;
using PlannerApp.Database;
using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;
using Microsoft.AspNetCore.Components;



namespace PlannerApp.Database.Repository;


public class ProjectsRepository : Repository<IDb, Project, Response>
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

    public override async Task<Response> CreateAsync(Project model)
    {
        try{

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
            return new Response(201, new Project{ ProjectName = model.ProjectName, ProjectDescription = model.ProjectDescription}, "Created");
            
        }catch(Exception e)
        {
            return new Response(500, new Project(), e.Message);
        }

    }

    public override  async Task<Response> GetAsync()
    {

        try{
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
                    ProjectDescription = string.IsNullOrEmpty(reader.GetString(2)) ? "No hay descripción" : reader.GetString(2)
                };
                projectList.Add(project);
            }
            await _dbService.CloseDb();
            return new Response(200,projectList, "Found");
        }
        catch(Exception e)
        {
            return new Response(500, new List<Project>(), e.Message);
        }
    }



    public override async Task<Response> UpdateAsync(Project model)
    {
        try{

            await _dbService.OpenDb();
            var id = await _local.GetId();
            using(var cmd = _dbService.GetCommand())
            {
                cmd.Connection = _dbService.GetProvider();
                cmd.CommandText = "UPDATE Projects SET ProjectName = @N, ProjectDescription = @D WHERE ProjectId = @P";
                cmd.Parameters.AddWithValue("@N",model.ProjectName);
                cmd.Parameters.AddWithValue("@P",model.ProjectId);
                cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
                //cmd.Parameters.AddWithValue("@R",user.UserRole);
                await cmd.ExecuteNonQueryAsync();
                
            }
            await _dbService.CloseDb();
            return new Response(204, new Project{ ProjectName = model.ProjectName, ProjectId = model.ProjectId}, "Updated");
            
        }catch(Exception e)
        {
            return new Response(500, new Project(), e.Message);
        }
    }
}