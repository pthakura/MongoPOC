
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace TutorialMongo.Repository
{
  public class EmployeeRepo
  {

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

    public void SaveEmployee(EmployeeDetails employee) {
      var collection = MongoDB.GetCollection<EmployeeDetails>("Employee");
      collection.InsertOne(employee);

    }

    public List<EmployeeDetails> GetEmployees(string name) {
      var collection = MongoDB.GetCollection<EmployeeDetails>("Employee");
      
      var listOfEmmployees =  collection.AsQueryable<EmployeeDetails>().Where(x => x.Name == name).ToList(); 

      return listOfEmmployees;

    }


  }
}
