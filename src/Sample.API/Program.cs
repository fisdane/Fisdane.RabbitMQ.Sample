using Fisdane.RabbitMQ;
using Fisdane.RabbitMQ.Options;
using Sample.API.Presentation.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMQOption>(builder.Configuration.GetSection("RabbitMQ"))
    .ValidateRabbitMQOptions();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.RegisterPublisher();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddUsersEndpoints();

app.Run();