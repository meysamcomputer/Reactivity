using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseContext>(opt=>opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();
try{
    using(var scope=app.Services.CreateScope())
    {
      var context=scope.ServiceProvider.GetRequiredService<DataBaseContext>();
      await context.Database.MigrateAsync();
      await Seed.SeedData(context);
    }
}
catch(Exception ex)
{
   var logger=app.Services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex,"An Error Occur During Migration");
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
