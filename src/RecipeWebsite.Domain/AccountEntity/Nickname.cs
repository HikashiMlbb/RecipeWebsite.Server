using RecipeWebsite.SharedKernel;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.AccountEntity;

public class Nickname
{
    public string Value { get; init; }

    private Nickname(string value)
    {
        Value = value;
    }

    public static Result<Nickname> Create(string value)
    {
        value = value.Trim();
        var validationResult = Validate(value);

        if (validationResult.IsSuccess)
        {
            return new Nickname(value);
        }

        return validationResult.Error!;
    }

    private static Result Validate(string value)
    {
        if (value.Length is < AccountConstraints.MinNicknameLength or > AccountConstraints.MaxNicknameLength)
        {
            return new Error("Nickname.InvalidLength", 
                $"Nickname length must be from {AccountConstraints.MinNicknameLength} to {AccountConstraints.MaxNicknameLength} symbols.");
        }
        
        var containsWhitespace = value.Any(char.IsWhiteSpace);
        var containsUnallowedSymbols = value.Any(x => @"!@#$%^&*()/\.?=+".Contains(x));
        
        if (containsWhitespace || containsUnallowedSymbols)
        {
            return new Error("Nickname.InvalidFormat", "Given nickname has invalid format.");
        }
        
        return Result.Success();
    }
}