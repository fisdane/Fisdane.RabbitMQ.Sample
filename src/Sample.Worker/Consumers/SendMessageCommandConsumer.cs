using MassTransit;
using Sample.Shared;

namespace Sample.Worker.Consumers;

internal class SendMessageCommandConsumer(ILogger<SendMessageCommandConsumer> logger) : IConsumer<SendMessageCommand>
{
    public async Task Consume(ConsumeContext<SendMessageCommand> context)
    {
        logger.LogInformation("Sending Email Message :: {email}", context.Message.Email);

        await Task.CompletedTask;
    }
}

internal class SendMessageCommandConsumerDefinition : ConsumerDefinition<SendMessageCommandConsumer>
{
    public SendMessageCommandConsumerDefinition()
    {
        // override the default endpoint name
       // EndpointName = SendMessageCommand.Queue;

        // limit the number of messages consumed concurrently
        // this applies to the consumer only, not the endpoint
        ConcurrentMessageLimit = 2;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SendMessageCommandConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        // configure message retry with millisecond intervals
        endpointConfigurator.UseMessageRetry(r => r.Interval(2, 20));

        // use the outbox to prevent duplicate events from being published
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}