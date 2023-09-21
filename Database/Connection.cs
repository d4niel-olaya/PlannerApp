

using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace PlannerApp.Database;


public class DatabaseProvider : IDb
{
    private readonly MySqlConnection _connection;


    public DatabaseProvider(MySqlConnection connection)
    {
        _connection = connection; 
    }


    public async Task OpenDb()
    {
        await _connection.OpenAsync();
    }

    public async Task CloseDb()
    {
        await _connection.CloseAsync();
    }
    

    public async Task<List<string>> GetQuery()
    {
       await OpenDb();
       var list = new List<string>();   
       var command = new MySqlCommand("SELECT * from Foo", _connection);
       var reader = await command.ExecuteReaderAsync();
        while(await reader.ReadAsync()) 
        {
            list.Add(reader.GetValue(0).ToString());
        }
        await CloseDb();
        return list;
    }


    public MySqlCommand GetCommand()
    {
        return new MySqlCommand();
    }

    public MySqlConnection GetProvider()
    {
        return _connection;
    }
    public ConnectionState GetState()
    {
        return _connection.State;
    }
    public bool IsOpen()
    {
        if(GetState() == ConnectionState.Open)
        {
            return true;   
        }
        else{
            return false;
        }
    }
}


public interface IDb
{
    Task OpenDb();
    Task<List<string>> GetQuery();
    Task CloseDb();
    MySqlCommand GetCommand();

    MySqlConnection GetProvider();
    ConnectionState GetState();
    bool IsOpen();
}
