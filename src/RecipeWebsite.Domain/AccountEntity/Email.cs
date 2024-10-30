using System.Text.RegularExpressions;
using RecipeWebsite.SharedKernel;

namespace RecipeWebsite.Domain.AccountEntity;

public partial class Email
{
    public string Value { get; set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        value = value.Trim();
        var validateResult = Validate(value);

        if (validateResult.IsSuccess)
        {
            return new Email(value);
        }

        return validateResult.Error!;
    }

    private static Result Validate(string value)
    {
        var emailRegex = EmailRegex();

        if (!emailRegex.IsMatch(value))
        {
            return new Error("Email.IsInvalid", "Given email has invalid format.");
        }
        
        return Result.Success();
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailRegex();
}