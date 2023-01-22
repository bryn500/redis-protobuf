using Redis.Client;
using Redis.MiddleWare;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints =
    {
        {"localhost", 6379 }
    },
    //ConnectTimeout = 10000,
    //AbortOnConnectFail = false
});

builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

builder.Services.AddScoped<ICacheClient, CacheClient>();

var app = builder.Build();

app.UsePlainText();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Enable Strict Transport Security
    app.UseHsts();

    // Redirect to https
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();
