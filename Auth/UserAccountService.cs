


using PlannerApp.Database;

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
        user.UserId = userByService.UserId;
        user.User = userByService.UserEmail;
        user.Role = userByService.UserRole;
        user.Password = userByService.UserPassword;
        return user;
    }
}