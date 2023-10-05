using PlannerApp.Database.Models;
using PlannerApp.Database.Repository;

namespace PlannerApp.Database.Services;

public class CommentService : ICommentService
{

     private readonly CommentsRepository _commentsRepository;

    public CommentService(CommentsRepository commentsRepository)
    {
         _commentsRepository = commentsRepository;
    }
    public async Task<Comments> CreateComment(Comments model)
    {
        var comment = await _commentsRepository.CreateAsync(model);
        return (Comments)comment.obj;
    }

    public async Task<List<Comments>> GetComments()
    {
        var commentsList = await _commentsRepository.GetAsync();
        return (List<Comments>)commentsList.obj;
    }

    public void SetIdTask(int id)
    {
        _commentsRepository.SetTaskId(id);
    }
}

public interface ICommentService
{
    Task<Comments> CreateComment(Comments model);
    Task<List<Comments>> GetComments();

    void SetIdTask(int id);
    
}