namespace PlannerApp.Helpers;
public class DataHelper
{
    public static string GetStringDB(IConfiguration configuration)
    {
        var devEnviroment = configuration["DbConnection:Db"];
        var productionEnviroment = Environment.GetEnvironmentVariable("DATABASE_URL");
        return string.IsNullOrEmpty(productionEnviroment) ? devEnviroment : productionEnviroment;
    }

    public static string GetApiUrl(IConfiguration configuration)
    {
        var devEnviroment = configuration["api_url"];
        var productionEnviroment = Environment.GetEnvironmentVariable("API_URL");
        return string.IsNullOrEmpty(productionEnviroment) ? devEnviroment : productionEnviroment;
    }
}