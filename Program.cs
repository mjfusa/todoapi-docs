/// <summary>
/// This file is the entry point for the TodoApi application. It sets up the web application builder,
/// configures services, and establishes database connections based on the environment.
/// </summary>

using Microsoft.EntityFrameworkCore; // Required for EF Core functionality.
using TodoApi.Models; // Includes the models used in the TodoApi.

var builder = WebApplication.CreateBuilder(args); // initializes the web application builder.

// Configure the app to use CORS (Cross-Origin Resource Sharing) to allow requests from the specified origin.
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                           // Replace the URL with the origin you wish to allow requests from.
                           policy.WithOrigins("https://patient-magpie-destined.ngrok-free.app");
                      });
});


// Database connection string configuration. The connection string varies based on the environment (Development or Production).

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    // In Development, the connection string is retrieved from appsettings.Development.json.
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("MyDbConnection");
}
else
{
      // In Production, the connection string is retrieved from environment variables.
      connection = Environment.GetEnvironmentVariable("MyDbConnection");
}

// Add services to the container.

builder.Services.AddControllers();
//   builder.Services.AddDbContext<TodoContext>(opt =>
//       opt.UseInMemoryDatabase("TodoList"));
  builder.Services.AddDbContext<TodoContext>(opt =>
      opt.UseSqlServer(connection));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.yaml", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseRouting(); // Add this line
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
