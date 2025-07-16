namespace Sample.API.Presentation.Users;

public class User
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Code { get; set; }
}


public record RegisterUserRequest(string Name, string Email);