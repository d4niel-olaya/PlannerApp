

namespace PlannerApp.Database.Models;


public class Comments
{
    public int CommentId {get;set;}

    public int CommentTaskId {get;set;}

    public int CommentUsersId {get;set;}

    public string Comment {get;set;} = string.Empty;
    public DateTime? CommentDate { get; set; }
}