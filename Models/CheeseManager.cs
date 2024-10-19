using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cheeseBackend.Models;

public class CheeseManager: ICheeseRepository<Cheese, string>
{
    private readonly IMongoCollection<Cheese> _cheeseCollection;

    public CheeseManager(IOptions<CheeseriaDatabaseSettings> cheeseriaDatabaseSettings)
    {
        var mongoClient = new MongoClient(cheeseriaDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(cheeseriaDatabaseSettings.Value.DatabaseName);
        _cheeseCollection = mongoDatabase.GetCollection<Cheese>(cheeseriaDatabaseSettings.Value.CheeseCollectionName);
    }
    
    public async Task<List<Cheese>> GetAllAsync() => await _cheeseCollection.Find(_ => true).ToListAsync();

    public async Task<Cheese?> GetByIdAsync(string id) => await _cheeseCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Cheese newCheese) => await _cheeseCollection.InsertOneAsync(newCheese);

    public async Task UpdateAsync(string id, Cheese updateCheese) => await _cheeseCollection.ReplaceOneAsync(c => c.Id == id, updateCheese);

    public async Task DeleteAsync(string id) => await _cheeseCollection.DeleteOneAsync(c => c.Id == id);
}