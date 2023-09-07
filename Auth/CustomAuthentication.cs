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
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}
