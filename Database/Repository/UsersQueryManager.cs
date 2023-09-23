

using PlannerApp.Database.Models;

namespace PlannerApp.Database;

public class UserQueryManager  : IUserQM
{
    private readonly IDb _dbservice;

   

    public UserQueryManager(IDb dbservice)
    {
        _dbservice  =   dbservice;
    }

    public async Task Register(User user)
    {
        await _dbservice.OpenDb();
        using(var cmd = _dbservice.GetCommand())
        {
            cmd.Connection = _dbservice.GetProvider();
            cmd.CommandText = "INSERT INTO Users(UserNickname, UserEmail, UserPassword, UserRole) VALUES(@N, @E, @P, @R)";
            cmd.Parameters.AddWithValue("@N",user.UserNickname);
            cmd.Parameters.AddWithValue("@E",user.UserEmail);
            cmd.Parameters.AddWithValue("@P",user.UserPassword);
            cmd.Parameters.AddWithValue("@R",user.UserRole);
            await cmd.ExecuteNonQueryAsync();
        }
        await _dbservice.CloseDb();
    }

    public async Task<User> GetUserAsync(string userName)
    {
        var users = new User();
      await _dbservice.OpenDb();
      using var cmd = _dbservice.GetCommand();
      cmd.Connection = _dbservice.GetProvider();
      cmd.CommandText = "SELECT UserId,UserEmail, UserRole, UserPassword FROM Users WHERE UserEmail = @UE LIMIT 1";
      cmd.Parameters.AddWithValue("@UE", userName);
      using var reader = await cmd.ExecuteReaderAsync();

        while(await reader.ReadAsync())
        {
                users.UserId = reader.GetInt32(0);
                users.UserEmail = reader.GetString(1);
                users.UserRole = reader.GetString(2);
                users.UserPassword = reader.GetString(3);
           
        }

        await _dbservice.CloseDb();
        return users;
    }

    
}

public interface IUserQM
{
    Task<User> GetUserAsync(string userName);
    Task Register(User user);

    
}