using PlannerApp.Auth.LocalStorage;
using PlannerApp.Database.Models;
using PlannerApp.Database;

namespace PlannerApp.Database.Repository;

public class CommentsRepository : Repository<IDb, Comments>
{

    private readonly IDb _dbService;
    private readonly UserLocalStorage _local;

    private int _taskId ;

    public CommentsRepository(IDb db, UserLocalStorage local)
    {
        _dbService = db;
        _local = local;
    }
    public override async Task<Comments> CreateAsync(Comments model)
    {
        await _dbService.OpenDb();
        var id = await _local.GetId();
        using (var cmd = _dbService.GetCommand())
        {
            cmd.Connection = _dbService.GetProvider();
            cmd.CommandText = "INSERT INTO CommentsTasks(CommentTaskId, CommentUsersId, CommentUsers) VALUES(@N, @D, @U)";
            cmd.Parameters.AddWithValue("@N", model.CommentTaskId);
            cmd.Parameters.AddWithValue("@D", id);
            cmd.Parameters.AddWithValue("@U", model.Comment);
            await cmd.ExecuteNonQueryAsync();
        }
        await _dbService.CloseDb();
        return new Comments { Comment = model.Comment, CommentTaskId = model.CommentTaskId };

    }
    public void SetTaskId(int taskId)
    {
        _taskId = taskId;
    }
    public override async  Task<List<Comments>> GetAsync()
    {
         var commentsList = new List<Comments>();
         await _dbService.OpenDb();
         using var cmd = _dbService.GetCommand();
        cmd.Connection = _dbService.GetProvider();
          cmd.CommandText = "SELECT CommentId,CommentTaskId, CommentUsersId, CommentUsers, Comment FROM CommentsTasks WHERE CommentTaskId = @N";
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
          return commentsList;
    }

    public override Task<Comments> UpdateAsync(Comments model)
    {
        throw new NotImplementedException();
    }
}