using Microsoft.EntityFrameworkCore;
using XmlGeneratorAPI.Data;
using XmlGeneratorAPI.Options;
using XmlGeneratorAPI.Services;
using XmlGeneratorAPI.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "XML Generator API",
        Version = "v1",
        Description = "EPCIS XML Generator API for GS1 Standard Events"
    });
});

// Options
builder.Services.Configure<UploadOptions>(builder.Configuration.GetSection("Uploads"));

// DbContext  
// 1. Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register DbContext with Pomelo MySQL provider
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<BizStepStrategyFactory>();

// CORS (optional - enable if needed for frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ensure database is created/migrated
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Ensure generated folder exists
var generatedPath = Path.Combine(app.Environment.WebRootPath ?? "wwwroot", "generated");
Directory.CreateDirectory(generatedPath);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "XML Generator API V1");
    });
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Serve static files from wwwroot to expose uploads and generated files
app.UseStaticFiles();

app.Run();