using System.Text.Json.Serialization;
using PlannerApp.Helpers;
using System.Text.Json;
namespace PlannerApp.Database.Services;


public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<FileItem>> GetFiles(int id)
    {
        var client = new HttpClient();
        var list = new List<FileItem>();
        var result = await client.GetAsync(DataHelper.GetApiUrl(_configuration)+"/taskfiles/"+id);
        if(result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadAsStringAsync();
 
            list = JsonSerializer.Deserialize<List<FileItem>>(response);

        }
        if(list == null)
        {
            return new List<FileItem>();
        }
        else{
            return list;
        }
    }

}

public interface IFileService
{
    Task<List<FileItem>> GetFiles(int id);
}

public class FileItem{
    public string Path {get;set;} = string.Empty;

    public string Name {get;set;} = string.Empty;
}