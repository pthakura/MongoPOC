using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TutorialMongo.Framewrok.Entity;

namespace TutorialMongo.Repository
{
  public class EmployeeDetails : BaseMongoModel
  {

    public int EmployeeID { get; set; }

    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Adddress Address { get; set; }
  }
}

public class Adddress
{
    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string Pincode { get; set; }  

}
