using PlannerApp.Database;
using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database.Temp;

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
    public override Task<Taskes> CreateAsync(Taskes model)
    {
        throw new NotImplementedException();
    }

    public override async Task<List<Taskes>> GetAsync()
    {
        var List = new List<Taskes>();
        var id = await _local.GetId();
        await _dbService.OpenDb();
        using var cmd = _dbService.GetCommand();
        cmd.Connection = _dbService.GetProvider();
        cmd.CommandText = "SELECT TaskId, TaskName, TaskDescription, TaskState FROM tasks WHERE TaskOwnerId = @U and TaskProjectId = @P";
        cmd.Parameters.AddWithValue("@U", id); // User session id
        cmd.Parameters.AddWithValue("@P", IdProject); // Project id 
        //cmd.Parameters.AddWithValue("@D",model.ProjectDescription);
        using var reader = await cmd.ExecuteReaderAsync();
         while(await reader.ReadAsync())
         {
            var task = new Taskes
            {
                TaskId = reader.GetInt32(0),
                TaskName = reader.GetString(1),
                TaskDescription = reader.GetString(2),
                TaskState = reader.GetString(3)
            };
            List.Add(task);
        }
        await _dbService.CloseDb();
        return List;
        
    }

    public override Task<Taskes> UpdateAsync(Taskes model)
    {
        throw new NotImplementedException();
    }
}