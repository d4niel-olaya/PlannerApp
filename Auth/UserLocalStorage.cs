
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace PlannerApp.Auth.LocalStorage;


public class UserLocalStorage
{
    private readonly ProtectedLocalStorage _local;


    public UserLocalStorage(ProtectedLocalStorage local)
    {
        _local = local;
    }

    public async Task<int> GetId()
    {
        var userId = await _local.GetAsync<int>("Id");
        var result = userId.Success ? userId.Value : 0;
        return result;
    }

    public async Task SetId(int id)
    {
        await _local.SetAsync("Id", id);
    }

    public async Task DeleteId()
    {
        await _local.DeleteAsync("Id");
    }
}
