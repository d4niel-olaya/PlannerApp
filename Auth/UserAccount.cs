

namespace PlannerApp.Auth;

public class UserAccount
{

    public int UserId {get;set;}
    public string User {get;set;} = string.Empty;

    public string Password {get;set;}  = string.Empty;

    public string Role {get;set;} = string.Empty;
}