using System.Text.RegularExpressions;
using RecipeWebsite.SharedKernel;

namespace RecipeWebsite.Domain.AccountEntity;

public partial class Password
{
    public string Value { get; init; }

    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password> Create(string value)
    {
        var validationResult = Validate(value);

        if (validationResult.IsSuccess)
        {
            return new Password(value);
        }

        return validationResult.Error!;
    }

    private static Result Validate(string value)
    {
        var passwordHashRegex = PasswordHashRegex();

        if (!passwordHashRegex.IsMatch(value))
        {
            return new Error("PasswordInvalid", "Given password hash has invalid format.");
        }
        
        return Result.Success();
    }

    [GeneratedRegex(@"^[a-f0-9]{64}$")]
    private static partial Regex PasswordHashRegex();
}