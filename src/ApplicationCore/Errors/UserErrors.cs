using ErrorOr;

namespace UtterlyComplete.ApplicationCore.Errors
{
    public static class UserErrors
    {
        public static Error EntityNotFound(string description = "Entity was not found.")
        {
            return Error.NotFound(
                code: $"{nameof(UserErrors)}.{nameof(EntityNotFound)}",
                description);
        }

        public static Error IllegalOperation(string description)
        {
            return Error.Forbidden(
                code: $"{nameof(UserErrors)}.{nameof(IllegalOperation)}",
                description: "You cannot complete this action.",
                metadata: new Dictionary<string, object>
                {
                    { "details", description }
                });
        }
    }
}
