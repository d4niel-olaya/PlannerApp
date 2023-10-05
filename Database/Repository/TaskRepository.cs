using PlannerApp.Database;
using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;
using System.Data;

namespace PlannerApp.Database.Repository;


public class TaskRepository : Repository<IDb, Taskes, Response>
{

    private readonly IDb _dbService;

    private int IdProject {get;set;}
    private readonly UserLocalStorage _local;   
    public TaskRepository(IDb db,UserLocalStorage local)
    {
        _dbService = db; 
        _local = local;   
    }
    public override async Task<Response> CreateAsync(Taskes model)
    {
        try{

            await _dbService.OpenDb();
            var id = await _local.GetId();
            using(var cmd = _dbService.GetCommand())
            {
                cmd.Connection = _dbService.GetProvider();
                cmd.CommandText = "INSERT INTO Tasks(TaskName, TaskDescription, TaskState, TaskOwnerId, TaskProjectId) VALUE(@N, @D, @S, @U, @P)";
                cmd.Parameters.AddWithValue("@N",model.TaskName);
                cmd.Parameters.AddWithValue("@D",model.TaskDescription);
                cmd.Parameters.AddWithValue("@S",model.TaskState);
                cmd.Parameters.AddWithValue("@U",id);
                cmd.Parameters.AddWithValue("@P", model.TaskProjectId);
                await cmd.ExecuteNonQueryAsync();
                
            }
            await _dbService.CloseDb();
            return new Response(200, new Taskes{TaskName = model.TaskName, TaskProjectId = model.TaskProjectId}, "Found");
            
        }catch(Exception e)
        {
            return new Response(500, new Taskes(), e.Message);
        }
    }

    public override async Task<Response> GetAsync()
    {
        try{

            var List = new List<Taskes>();
            var id = await _local.GetId();
            await _dbService.OpenDb();
            using var cmd = _dbService.GetCommand();
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "SELECT TaskId, TaskName, TaskState , IFNULL(TaskDescription, 'No hay una descripción') as TaskDescription FROM Tasks WHERE TaskOwnerId = @U and TaskProjectId = @P";
            cmd.Parameters.AddWithValue("@U", id); // User session id
            cmd.Parameters.AddWithValue("@P", IdProject); // Project id 
            
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                var task = new Taskes
                {
                    TaskId = reader.GetInt32(0),
                    TaskName = reader.GetString(1),
                    TaskState = reader.GetString(2),
                    TaskDescription = reader.GetString(3)
                };
                List.Add(task);
            }
            await _dbService.CloseDb();
            return new Response(200, List, "Found");
            
        }catch(Exception e)
        {
            return new Response(500,  new List<Taskes>(), e.Message);
        }
        
    }
    public void SetId(int idpr)
    {
        IdProject = idpr;
    }
    public override async Task<Response> UpdateAsync(Taskes model)
    {
        try{

            await _dbService.OpenDb();
            using(var cmd = _dbService.GetCommand())
            {
                cmd.Connection = _dbService.GetProvider();
                cmd.CommandText = "UPDATE Tasks SET TaskState = @N WHERE TaskId = @T";
                cmd.Parameters.AddWithValue("@N",model.TaskState);
                cmd.Parameters.AddWithValue("@T",model.TaskId);
                //cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
            //cmd.Parameters.AddWithValue("@U",id);
                //cmd.Parameters.AddWithValue("@R",user.UserRole);
                await cmd.ExecuteNonQueryAsync();
                
            }
            await _dbService.CloseDb();
            return new Response(204, new Taskes{TaskId = model.TaskId}, "Updated");
            
        }
        catch(Exception e)
        {
            return new Response(500, new Taskes(), e.Message);
        }
        
    }
}