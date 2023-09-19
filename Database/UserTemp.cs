
// save user id when the user is logged

namespace PlannerApp.Database.Temp;
public class UserTemp
{

    private  int _currentUser;
    public void SetCurrentUserId(int _userid)
    {
        _currentUser = _userid;
    }

    public int GetCurrentUserId()
    {
        return _currentUser;
    }
}