

using PlannerApp.Database;

public abstract class Repository<TDbService,TModel, TResponse> where TDbService : IDb where TResponse : Response            
{
    public abstract Task<Response> GetAsync();

    public abstract Task<TResponse> CreateAsync(TModel model);

    public abstract Task<TResponse> UpdateAsync(TModel model);

    

}    


public class Response{

    public int code {get;set;}

    public object obj {get;set;}

    public string msg {get;set;} = string.Empty;

    public Response(int c, object ob, string m)
    {
        code = c;
        obj = ob;
        msg = m;
    }
}