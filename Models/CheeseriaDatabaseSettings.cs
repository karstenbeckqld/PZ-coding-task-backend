namespace cheeseBackend.Models;

// This model describes the databse settings required to access the MongoDB database and defined in appsettings.json. 
public class CheeseriaDatabaseSettings
{
    public string ConnectionString { get; set; }
    
    public string DatabaseName { get; set; }
    
    public string CheeseCollectionName { get; set; }
}