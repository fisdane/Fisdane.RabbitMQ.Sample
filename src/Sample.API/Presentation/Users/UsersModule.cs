using Fisdane.RabbitMQ.Configuration;
using Sample.Shared;

namespace Sample.API.Presentation.Users;

public static class UsersModule
{
    public static void AddUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", (IUserService userService) =>
        {
            List<User> users = userService.GetUsers();

            return Results.Ok(new
            {
                message = "User retrieved successfully",
                data = users
            });
        })
        .WithName("GetUsers")
        .WithOpenApi();

        app.MapPost("/users", async (RegisterUserRequest request, IUserService userService, IPublishEvent publishEvent) =>
        {
            User newUser = new()
            {
                Name = request.Name,
                Email = request.Email,
                Code = Random.Shared.Next(100, 700)
            };
            userService.RegisterUser(newUser);

            // we want to publish an event that a new user has been registered and do some processing
            UserRegisteredEvent _event = new(request.Name, DateTime.Now);
            await publishEvent.Publish<UserRegisteredEvent>(_event, _event.GetType()).ConfigureAwait(false);

            return Results.Ok(new
            {
                message = "User registered successfully"
            });
        })
        .WithName("RegisterUser")
        .WithOpenApi();

        app.MapDelete("/users/{name}", async (string name, IUserService userService, ISendMessage sendMessage) =>
        {
            User? user = userService.GetUser(name);
            if (user is null)
            {
                return Results.NotFound(new
                {
                    message = "User not registered"
                });
            }

            userService.DeleteUser(user);

            // we want to send an email saying goodbye to user
            SendMessageCommand command = new(user.Email);
            await sendMessage.SendMessageAsync<SendMessageCommand>(SendMessageCommand.Queue, command).ConfigureAwait(false);

            return Results.Ok(new
            {
                message = "User deleted successfully"
            });
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}