

using PlannerApp.Database;

public abstract class Repository<TDbService,TModel> where TDbService : IDb             
{
    public abstract Task<List<TModel>> GetAsync();

    public abstract Task<TModel> CreateAsync(TModel model);

    public abstract Task<TModel> UpdateAsync(TModel model);

    

}        