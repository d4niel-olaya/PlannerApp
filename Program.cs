using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using PlannerApp.Areas.Identity;
using PlannerApp.Data;
using PlannerApp.Helpers;
using MudBlazor.Services;
using MySqlConnector;
using PlannerApp.Database;
using PlannerApp.Database.Temp;
using PlannerApp.Auth;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PlannerApp.Database.Repository;
using PlannerApp.Database.Services;
using PlannerApp.Auth.LocalStorage;
using System.Net.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();

builder.Services.AddTransient(_ =>
    new MySqlConnection(DataHelper.GetStringDB(builder.Configuration))
    );
builder.Services.AddTransient<IDb,DatabaseProvider>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  //  .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticacion>(); // Inject auth service
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<UserLocalStorage>();
builder.Services.AddScoped<IUserQM,UserQueryManager>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddSingleton<UserTemp>();
builder.Services.AddScoped<ProjectsRepository>(); 
builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
