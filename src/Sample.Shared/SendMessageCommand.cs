namespace Sample.Shared
{
    public class SendMessageCommand(string email)
    {
        public const string Queue = nameof(SendMessageCommand);
        public string Email { get; } = email;
    }
}