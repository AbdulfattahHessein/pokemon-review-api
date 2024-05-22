using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Configuration;
using PokemonReviewApp.Data;
using ServiceCollectionAccessorService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(optionsAction =>
{
    optionsAction.UseSqlServer(builder.Configuration["ConnectionStrings:pokemonDb"]);
});

// Repositories Services
builder.Services.AddRepositoriesServices();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddServiceCollectionAccessor();

var app = builder.Build();


using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();

    try
    {
        await context.Database.MigrateAsync();

        await context.SeedDataAsync();
    }
    catch (Exception)
    {
        await context.Database.EnsureDeletedAsync();

        await context.Database.MigrateAsync();

        await context.SeedDataAsync();
    }
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
