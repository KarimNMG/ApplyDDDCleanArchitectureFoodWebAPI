using ErrorOr;

namespace Project.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvlaidCredentials => Error.Validation(
            code: "Auth.InvalidCredential",
            description: "Invalid Credentials");
    }
}