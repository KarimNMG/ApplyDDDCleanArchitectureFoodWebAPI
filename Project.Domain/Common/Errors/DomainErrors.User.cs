using ErrorOr;

namespace Project.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error DuplicatEmail => Error.Conflict(code: "User.DuplicateEmail", description: "Email already exist");
        public static Error UserNotFound => Error.NotFound(code: "User.NotFound", description: "User can not found");
    }
}