using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
public class User{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("username")]
    public string Username { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("productIds")]
    public List<string> ProductIds { get; set; } = new List<string>();
}