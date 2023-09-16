
namespace PlannerApp.Database.Models;
public class User
{
    public int UserId {get;set;}

    public string UserNickname {get;set;} =string.Empty;

    public string UserEmail {get;set;} = string.Empty;

    public string UserPassword {get;set;} =string.Empty;

    public string UserRole {get;set;} =string.Empty;
}