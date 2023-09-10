using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace PlannerApp.Auth;

public class CustomAuthenticacion : AuthenticationStateProvider
{

    private readonly ProtectedSessionStorage _sessionStorage;

    private  ClaimsPrincipal   _claims = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticacion(ProtectedSessionStorage session)
    {
        _sessionStorage = session;
    }
    public override  async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try{
            var userSession = await  _sessionStorage.GetAsync<UserSession>("UserSession");
        var userSessionResult= userSession.Success ? userSession.Value : null;

        if(userSessionResult==null)
        {
            return await Task.FromResult(new AuthenticationState(_claims));
        }

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>(){
                new Claim(ClaimTypes.Name, userSessionResult.UserName),
                new Claim(ClaimTypes.Role,userSessionResult.Role)
            
            },"CustomAuth"
        ));
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
             return await Task.FromResult(new AuthenticationState(_claims));
        }
    }

    public async Task UpdateAuthenticacionState(UserSession userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if(userSession != null)
        {
            await _sessionStorage.SetAsync("UserSession", userSession);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>(){
                new Claim(ClaimTypes.Name, userSession.UserName),
                new Claim(ClaimTypes.Role,userSession.Role)
            
            }
        ));

        }else
        {
            await _sessionStorage.DeleteAsync("UserSession");
            claimsPrincipal = _claims;
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}
