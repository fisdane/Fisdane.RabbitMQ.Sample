using Sample.Worker.Consumers;

namespace Sample.Worker;

internal static class ConsumerMapping
{
    public static Dictionary<string, (Type, Type)> Load()
    {
        return new Dictionary<string, (Type, Type)>
        {
            ["SendMessage"] = (typeof(SendMessageCommandConsumer), typeof(SendMessageCommandConsumerDefinition)),
            ["UserRegistered"] = (typeof(UserRegisteredEventConsumer), typeof(UserRegisteredEventConsumerDefinition))
        };
    }
}