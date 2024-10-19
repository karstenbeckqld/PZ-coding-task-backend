using cheeseBackend.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CheeseriaDatabaseSettings>(
    builder.Configuration.GetSection("CheeseDatabase"));

// Register the repository and service
//builder.Services.AddSingleton<ICheeseRepository<Cheese, string>, CheeseRepository>();

//builder.Services.AddSingleton<CheeseriaService>();
builder.Services.AddSingleton<CheeseManager>();
// Add services to the container
//builder.Services.AddScoped<IImageRepository<IFormFile>, ImageManager>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Enable static files
app.UseStaticFiles(); // This allows serving files from wwwroot or other directories

// Optionally, enable static files from specific directories
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "public")),
    RequestPath = "/public"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();