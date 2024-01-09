using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace TutorialMongo.Framewrok.Entity
{
  public class BaseMongoModel
  {

    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string Id { get; set; }

    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; }

    [BsonElement("modifiedDate")]
    public DateTime ModifiedDate { get; set; }
  }
}
