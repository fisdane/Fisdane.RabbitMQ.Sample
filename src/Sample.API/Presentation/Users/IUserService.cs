namespace Sample.API.Presentation.Users;

internal interface IUserService
{
    void RegisterUser(User user);

    void DeleteUser(User user);

    User? GetUser(string name);

    List<User> GetUsers();
}

internal class UserService : IUserService
{
    private readonly List<User> _users =
    [
        new()
        {
            Name = "User A", Email = "user.a@gmail.com", Code = 254
        },
        new()
        {
            Name = "User B", Email = "user.b@gmail.com", Code = 521
        }
    ];

    public List<User> GetUsers()
    {
        return _users;
    }

    public User? GetUser(string name)
    {
        User? user = _users.Find(u => u.Name.Trim() == name.Trim());

        return user;
    }

    public void RegisterUser(User user)
    {
        _users.Add(user);
    }

    public void DeleteUser(User user)
    {
        _users.Remove(user);
    }
}