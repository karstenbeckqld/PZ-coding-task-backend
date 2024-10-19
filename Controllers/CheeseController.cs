using cheeseBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace cheeseBackend.Controllers;

// The Cheese controller handles all crud operations for cheese objects and operates on the MongoDB database. It is
// based on the repository design pattern and  injects the CheeseManager which implements the ICheeseRepository. 
[ApiController]
[Route("api/[controller]")]
public class CheeseController(CheeseManager repo) : ControllerBase
{
    [HttpGet] // Get all cheeses in the database
    public async Task<List<Cheese>> GetAll() => await repo.GetAllAsync();

    [HttpGet("{id:length(24)}")] // Get a cheese by its ID.
    public async Task<ActionResult<Cheese>> GetCheeseById(string id)
    {
       var cheese = await repo.GetByIdAsync(id);

       if (cheese == null)
       {
           return NotFound();
       }

       return cheese;
    }

    [HttpPost] // Save a cheese to the database.
    public async Task<IActionResult> Post(Cheese newCheese)
    {
        Console.Out.WriteLine(newCheese);
        await repo.CreateAsync(newCheese); // Save the cheese data with the image URL
        return CreatedAtAction(nameof(Post), new { id = newCheese.Id }, newCheese);
    }

    [HttpPut("{id:length(24)}")] // Update a cheese in the database.
    public async Task<IActionResult> Put(string id, [FromBody] Cheese updateCheese)
    {
        var cheese = await repo.GetByIdAsync(id);
        
        if (cheese == null)
            return NotFound();
        
        updateCheese.Id = cheese.Id;
        
        await repo.UpdateAsync(id, updateCheese);
        
        return NoContent();
    }
    
    [HttpDelete("{id:length(24)}")] // Delete a cheese from the database.
    public async Task<IActionResult> Delete(string id)
    {
        var cheese = await repo.GetByIdAsync(id);
        
        if (cheese == null)
            return NotFound();
        
        await repo.DeleteAsync(id);
        
        return NoContent();
    }
}