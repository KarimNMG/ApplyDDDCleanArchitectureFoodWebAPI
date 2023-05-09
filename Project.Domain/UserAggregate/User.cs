namespace Project.Domain.UserAggregate;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User CreateUser(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return new User(
            id,
            firstName,
            lastName,
            email,
            password);
    }

    public void SetUserFirstName(string firstName)
    {
        this.FirstName = firstName;
    }

    public void SetUserLastName(string lastName)
    {
        this.LastName = lastName;
    }
}