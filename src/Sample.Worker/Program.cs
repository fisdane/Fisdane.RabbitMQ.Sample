using Fisdane.RabbitMQ;
using Fisdane.RabbitMQ.Options;
using Sample.Worker;

var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddHostedService<Worker>();

var env = builder.Environment.EnvironmentName;
Console.WriteLine($"HostingEnvironment : {env}");

builder.Configuration.Sources.Clear();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

if (args != null)
    builder.Configuration.AddCommandLine(args);

builder.Services
    .Configure<RabbitMQOption>(builder.Configuration.GetSection("RabbitMQ"))
    .ValidateRabbitMQOptions();

builder.Services.RegisterConsumers(builder.Configuration, ConsumerMapping.Load);

var host = builder.Build();
host.Run();