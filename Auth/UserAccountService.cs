


using PlannerApp.Database;
using PlannerApp.Database.Models;

namespace PlannerApp.Auth;

public class UserAccountService
{
    
    private readonly IUserQM _userService;

    public UserAccountService(IUserQM userService)
    {       
       _userService =   userService;
    }

    public async Task<UserAccount> GetUser(string UserName)
    {
        var user = new UserAccount();
        var userByService = await _userService.GetUserAsync(UserName);
        var result = (User)userByService.obj;
        user.UserId = result.UserId;
        user.User = result.UserEmail;
        user.Role = result.UserRole;
        user.Password = result.UserPassword;
        return user;
    }
}