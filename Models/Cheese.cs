using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace cheeseBackend.Models;

// The Cheese model describes the cheese object as stored in the database. The Id gets set by the database and is a
// string as we're using MongoDB.
public class Cheese
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    
    public string Image { get; set; } = string.Empty;
    
    public string Colour { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public string[] Tags { get; set; } = Array.Empty<string>();
    
    public string Description { get; set; } = string.Empty;
}