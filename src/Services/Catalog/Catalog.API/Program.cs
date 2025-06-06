var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

// Add MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));

// Add Authorization
// builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

// Add authorization middleware
// app.UseAuthorization();

// Add logging middleware
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
    await next();
});

app.Run();
