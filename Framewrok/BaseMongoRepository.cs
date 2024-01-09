using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using TutorialMongo.Framewrok.Contract;
using TutorialMongo.Framewrok.Entity;

namespace TutorialMongo.Framewrok
{
  public class BaseMongoRepository : IBaseMongoRepository
  {

    private readonly string _connectionstring;

    public BaseMongoRepository() {
    }


    protected IMongoDatabase MongoDB
    {
      get
      {
        MongoUrl mongoUrl = new MongoUrl("mongodb://localhost:27017/tutorialMongo");
        MongoClientSettings settings = MongoClientSettings.FromUrl(mongoUrl);
        double connectionTimeout = 30000;
        TimeSpan ts = TimeSpan.FromMilliseconds(connectionTimeout);
        settings.ConnectTimeout = ts;
        settings.ServerSelectionTimeout = ts;

        MongoClient client = new MongoClient(settings);

        IMongoDatabase db = client.GetDatabase(mongoUrl.DatabaseName);
        return db;

      }
    }


    public T FindOneAndUpdate<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      T updateData = collection.FindOneAndUpdate(filter, UpdateAuditProperties<T>(update), options);
      return updateData;
    }

    public List<T> LoadData<T>(string collectionName, FilterDefinition<T> filter, int? count = null, int? skip = null) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      return  collection.Find<T>(filter).Limit(count).Skip(skip).ToList();
    }

    public IMongoCollection<T> RemoveData<T>(string collectionName, FilterDefinition<T> filter) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      collection.DeleteMany(filter);
      return collection;
    }

    public T InsertData<T>(T data, string collectionName) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      collection.InsertOne(AddBaseMongoData(data));
      return data;
    }

    public List<T> InsertManyData<T>(List<T> data, string collectionName) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      collection.InsertMany(AddBaseMongoDataList(data));
      return data;
    }

    public void UpdateData<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null) {
      IMongoCollection<T> collection = MongoDB.GetCollection<T>(collectionName);
      collection.UpdateMany(filter, UpdateAuditProperties<T>(update), options);
    }

    private T AddBaseMongoData<T>(T model) {
      if (model is BaseMongoModel) {
        DateTime utc = DateTime.UtcNow;
        (model as BaseMongoModel).CreatedDate = utc;
      }
      return model;
    }

    private List<T> AddBaseMongoDataList<T>(List<T> models) {

      foreach (var model in models) {
        if (model is BaseMongoModel) {
          DateTime utc = DateTime.UtcNow;
          (model as BaseMongoModel).CreatedDate = utc;
        }
      }
      return models;
    }

    private UpdateDefinition<T> UpdateAuditProperties<T>(UpdateDefinition<T> updateDefinition) {
      if (typeof(T).IsSubclassOf(typeof(BaseMongoModel))) {
        DateTime utc = DateTime.UtcNow;
        updateDefinition = updateDefinition.Set(u => (u as BaseMongoModel).ModifiedDate, utc);
      }

      return updateDefinition;
    }
  }
}
