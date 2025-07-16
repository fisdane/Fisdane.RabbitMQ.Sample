using MassTransit;
using Sample.Shared;

namespace Sample.Worker.Consumers;

internal class UserRegisteredEventConsumer(ILogger<UserRegisteredEventConsumer> logger) : IConsumer<UserRegisteredEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var _event = context.Message;

        logger.LogInformation("Processing user registration :: {eventName} registered on {eventAddedOn}", _event.Name, _event.AddedOn);
        await Task.CompletedTask;
    }
}

internal class UserRegisteredEventConsumerDefinition : ConsumerDefinition<UserRegisteredEventConsumer>
{
    public UserRegisteredEventConsumerDefinition()
    {
        // override the default endpoint name
        //EndpointName = nameof(UserRegisteredEvent);

        // limit the number of messages consumed concurrently
        // this applies to the consumer only, not the endpoint
        ConcurrentMessageLimit = 2;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UserRegisteredEventConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(20)));
        endpointConfigurator.PrefetchCount = 2;
    }
}