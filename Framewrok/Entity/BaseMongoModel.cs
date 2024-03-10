using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TutorialMongo.Framewrok.Entity
{
  public class BaseMongoModel
  {

    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    [JsonIgnore]
    public string? Id { get; set; }

    [BsonElement("createdDate")]
    [JsonIgnore]
    public DateTime? CreatedDate { get; set; }

    [BsonElement("modifiedDate")]
    [JsonIgnore]
    public DateTime? ModifiedDate { get; set; }
  }
}
