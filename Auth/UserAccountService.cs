


namespace PlannerApp.Auth;

public class UserAccountService
{
    private List<UserAccount> listUsers;

    public UserAccountService()
    {
        listUsers = new List<UserAccount>()
        {
            new UserAccount{ User = "admin", Password="admin", Role="admin"}
        };
    }

    public UserAccount? GetUser(string UserName)
    {
        return listUsers.FirstOrDefault(u => u.User == UserName);
    }
}