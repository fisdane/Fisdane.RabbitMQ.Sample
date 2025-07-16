namespace Sample.Shared
{
    public class UserRegisteredEvent(string name, DateTime addedOn)
    {
        public string Name { get; } = name;
        public DateTime AddedOn { get; } = addedOn;
    }
}