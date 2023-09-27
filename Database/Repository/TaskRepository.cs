using PlannerApp.Database;
using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;
using System.Data;

namespace PlannerApp.Database.Repository;


public class TaskRepository : Repository<IDb, Taskes>
{

    private readonly IDb _dbService;

    private int IdProject {get;set;}
    private readonly UserLocalStorage _local;   
    public TaskRepository(IDb db,UserLocalStorage local)
    {
        _dbService = db; 
        _local = local;   
    }
    public override async Task<Taskes> CreateAsync(Taskes model)
    {
        await _dbService.OpenDb();
        var id = await _local.GetId();
        using(var cmd = _dbService.GetCommand())
        {
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "INSERT INTO tasks(TaskName, TaskDescription, TaskState, TaskOwnerId) VALUE(@N, @D, @S, @U)";
            cmd.Parameters.AddWithValue("@N",model.TaskName);
            cmd.Parameters.AddWithValue("@D",model.TaskDescription);
            cmd.Parameters.AddWithValue("@S",model.TaskState);
            cmd.Parameters.AddWithValue("@U",id);
            await cmd.ExecuteNonQueryAsync();
            
        }
        await _dbService.CloseDb();
        return new Taskes{TaskName = model.TaskName, TaskProjectId = model.TaskProjectId};
    }

    public override async Task<List<Taskes>> GetAsync()
    {
        var List = new List<Taskes>();
        var id = await _local.GetId();
        await _dbService.OpenDb();
        using var cmd = _dbService.GetCommand();
        cmd.Connection = _dbService.GetProvider();
        cmd.CommandText = "SELECT TaskId, TaskName, TaskState FROM tasks WHERE TaskOwnerId = @U and TaskProjectId = @P";
        cmd.Parameters.AddWithValue("@U", id); // User session id
        cmd.Parameters.AddWithValue("@P", IdProject); // Project id 
        
        using var reader = await cmd.ExecuteReaderAsync();
         while(await reader.ReadAsync())
         {
            var task = new Taskes
            {
                TaskId = reader.GetInt32(0),
                TaskName = reader.GetString(1),
                TaskState = reader.GetString(2)
            };
            List.Add(task);
        }
        await _dbService.CloseDb();
        return List;
        
    }
    public void SetId(int idpr)
    {
        IdProject = idpr;
    }
    public override async Task<Taskes> UpdateAsync(Taskes model)
    {
        await _dbService.OpenDb();
        using(var cmd = _dbService.GetCommand())
        {
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "UPDATE tasks SET TaskState = @N WHERE TaskId = @T";
            cmd.Parameters.AddWithValue("@N",model.TaskState);
            cmd.Parameters.AddWithValue("@T",model.TaskId);
            //cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
           //cmd.Parameters.AddWithValue("@U",id);
            //cmd.Parameters.AddWithValue("@R",user.UserRole);
            await cmd.ExecuteNonQueryAsync();
            
        }
        await _dbService.CloseDb();
        return new Taskes{TaskId = model.TaskId};
    }
}