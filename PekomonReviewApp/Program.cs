using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Configuration;
using PokemonReviewApp.Data;
using ServiceCollectionAccessorService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Seed>();


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


#region Seed Data

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedAppDbContext();
    }
}

#endregion


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
