var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("CatalogConnection")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

// Add logging middleware
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
    await next();
});

app.Run();
