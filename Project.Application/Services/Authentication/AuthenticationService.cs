﻿namespace Project.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Login(string Email, string Password)
    {
        return new AuthenticationResult(new Guid(), "FirstName", "LastName", "Email", "Token");
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        return new AuthenticationResult(new Guid(), FirstName, LastName, Email, "Token");
    }
}
