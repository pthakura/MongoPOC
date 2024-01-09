using MongoDB.Driver;

namespace TutorialMongo.Framewrok.Contract
{
  public interface IBaseMongoRepository
  {
    T InsertData<T>(T data, string collectionName);

    List<T> LoadData<T>(string collectionName, FilterDefinition<T> filter, int? count = null, int? skip = null);

    IMongoCollection<T> RemoveData<T>(string collectionName, FilterDefinition<T> filter);

    void UpdateData<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null);

    T FindOneAndUpdate<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

    List<T> InsertManyData<T>(List<T> data, string collectionName);
  }
}
