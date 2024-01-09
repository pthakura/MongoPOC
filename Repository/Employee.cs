using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TutorialMongo.Framewrok.Entity;

namespace TutorialMongo.Repository
{
  public class EmployeeDetails:BaseMongoModel
  {
    
    public int EmployeeID { get; set; }

    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
