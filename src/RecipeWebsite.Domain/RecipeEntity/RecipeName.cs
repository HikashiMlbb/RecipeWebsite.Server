using RecipeWebsite.SharedKernel;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.RecipeEntity;

public class RecipeName
{
    public string Name { get; }

    private RecipeName(string name)
    {
        Name = name;
    }

    public static Result<RecipeName> Create(string name)
    {
        name = name.Trim();
        var validating = Validate(name);
        
        return validating.IsFailure
            ? validating.Error!
            : new RecipeName(name);
    }

    private static Result Validate(string name)
    {
        if (name.Length > RecipeConstraints.MaxNameLength)
        {
            return new Error("RecipeName.MaxNameLength", "Recipe name length is more than max name length constraint.");
        }
            
        if (name.Contains("<script>") || name.Contains("</script>"))
        {
            return new Error("RecipeName.NotAllowedTag", "Recipe name contains not allowed tag which is used by XSS.");
        }

        return Result.Success();
    }
}