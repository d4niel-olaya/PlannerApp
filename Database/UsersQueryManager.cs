

using PlannerApp.Database.Models;

namespace PlannerApp.Database;

public class UserQueryManager 
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
}

public interface IUserQM
{

}