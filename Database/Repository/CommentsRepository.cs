using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database;

namespace PlannerApp.Database.Repository;

public class CommentsRepository : Repository<IDb, Comments, Response>
{

    private readonly IDb _dbService;
    private readonly UserLocalStorage _local;

    private int _taskId ;

    public CommentsRepository(IDb db, UserLocalStorage local)
    {
        _dbService = db;
        _local = local;
    }
    public override async Task<Response> CreateAsync(Comments model)
    {

        try{

            await _dbService.OpenDb();
            var id = await _local.GetId();
            using (var cmd = _dbService.GetCommand())
            {
                cmd.Connection = _dbService.GetProvider();
                cmd.CommandText = "INSERT INTO CommentsTasks(CommentTaskId, CommentUsersId, Comment) VALUES(@N, @D, @U)";
                cmd.Parameters.AddWithValue("@N", model.CommentTaskId);
                cmd.Parameters.AddWithValue("@D", id);
                cmd.Parameters.AddWithValue("@U", model.Comment);
                await cmd.ExecuteNonQueryAsync();
            }
            await _dbService.CloseDb();
            return new Response(201, new Comments { Comment = model.Comment, CommentTaskId = model.CommentTaskId }, "Created");
        }
        catch(Exception e)
        {
            return new Response(500, new Comments(), e.Message);
        }
      

    }
    public void SetTaskId(int taskId)
    {
        _taskId = taskId;
    }
    public override async  Task<Response> GetAsync()
    {

        try{

            var commentsList = new List<Comments>();
            await _dbService.OpenDb();
            using var cmd = _dbService.GetCommand();
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "SELECT CommentId,CommentTaskId, CommentUsersId, Comment FROM CommentsTasks WHERE CommentTaskId = @N";
            cmd.Parameters.AddWithValue("@N",_taskId);
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                var comment = new Comments{
                    CommentId = reader.GetInt32(0),
                    CommentTaskId = reader.GetInt32(1),
                    CommentUsersId = reader.GetInt32(2),
                    Comment = reader.GetString(3)
                };
                commentsList.Add(comment);
            }
            await _dbService.CloseDb();
            return new Response(200, commentsList, "Found");
        }
        catch(Exception e)
        {
            return new Response(500, new List<Comments>(), e.Message);
        }
        
    }

    public override Task<Response> UpdateAsync(Comments model)
    {
        throw new NotImplementedException();
    }
}