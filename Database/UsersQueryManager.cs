

namespace PlannerApp.Database;

public class UserQueryManager 
{
    private readonly IDb _dbservice;

    public UserQueryManager(IDb dbservice)
    {
        _dbservice  =   dbservice;
    }
}

public interface IUserQM
{

}