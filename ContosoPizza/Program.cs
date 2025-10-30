using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Local API",
        Version = "v1"
    });
});

// Optional: Add CORS for browser access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHttpsRedirection(); // can comment out if causing issues on Linux
app.UseCors("AllowAll");
app.UseAuthorization();

// Enable Swagger for all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{(app.Environment.IsDevelopment() ? string.Empty : "~")}/swagger/v1/swagger.json", "My Local API V1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
