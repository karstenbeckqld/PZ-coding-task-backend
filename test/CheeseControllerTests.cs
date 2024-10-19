using cheeseBackend.Controllers;
using cheeseBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace cheeseBackend.test;

public class CheeseControllerTests
{
    private readonly CheeseController _cheeseController;

    public CheeseControllerTests()
    {
        // Create mock settings
        Mock<IOptions<CheeseriaDatabaseSettings>> mockOptions = new();

        // Setup the values you need in your CheeseriaDatabaseSettings
        var settings = new CheeseriaDatabaseSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "Cheeseria",
            CheeseCollectionName = "Cheeses"
        };

        mockOptions.Setup(s => s.Value).Returns(settings);

        // Instantiate CheeseManager with mock settings
        var cheeseManager = new CheeseManager(mockOptions.Object);

        // Create the controller instance with the CheeseManager
        _cheeseController = new CheeseController(cheeseManager);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfCheeses()
    {
        // Act
        var result = await _cheeseController.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Cheese>>(result);
    }

    [Fact]
    public async Task GetCheeseById_ReturnsCheese_WhenCheeseExists()
    {
        // Arrange
        var cheeseId = "6713e5d84c5f28649bbe6ea0"; // Id from Database
        var expectedCheese = new Cheese
        {
            Id = cheeseId,
            Name = "Goat on a Hot Tin Roof",
            Image = "goat.jpg",
            Colour = "White",
            Price = 35.29m,
            Tags = new string[] { "Made from pasteurized goat's milk", "Country of origin: Australia", "Type: fresh firm, artisan", "Texture: creamy and soft", "Rind: rindless", "Flavour: herbaceous, spicy", "Aroma: fresh, spicy", "Producers: Woodside Cheese Wrights" },
            Description = "his quirkily named cheese from Woodside, Australia is made using milk specially sourced from Towerview and Oskjberg goat dairies. It is infused with locally grown combination of chilli, saltbush, and native pepperberry and crushed tanami apples. Serve Goat on a Hot Tin Roof with warm crusty bread and a drizzle of olive oil.."
        };

        // Act
        var result = await _cheeseController.GetCheeseById(cheeseId);

        // Assert
        var okResult = Assert.IsType<ActionResult<Cheese>>(result);
        var actualCheese = Assert.IsType<Cheese>(okResult.Value);

        Assert.Equal(expectedCheese.Id, actualCheese.Id);
        Assert.Equal(expectedCheese.Name, actualCheese.Name);
        Assert.Equal(expectedCheese.Image, actualCheese.Image);
    }
    
    [Fact]
    public async Task GetCheeseById_ReturnsNotFound_WhenCheeseDoesNotExist()
    {
       // Arrange
    var cheeseId = "507f1f77bcf86cd799439011"; // Example ID for a non-existent cheese

    // Act
    var result = await _cheeseController.GetCheeseById(cheeseId);

    // Assert
    Assert.IsType<NotFoundResult>(result.Result); // Expect a 404 NotFound response
    }
}