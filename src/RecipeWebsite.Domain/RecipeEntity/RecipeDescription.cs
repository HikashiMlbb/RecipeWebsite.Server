using RecipeWebsite.SharedKernel;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.RecipeEntity;

public class RecipeDescription
{
    public string Description { get; }
    
    private RecipeDescription(string description)
    {
        Description = description;
    }

    public static Result<RecipeDescription> Create(string description)
    {
        description = description.Trim();
        var validating = Validate(description);

        return validating.IsFailure
            ? validating.Error!
            : new RecipeDescription(description);
    }

    private static Result Validate(string description)
    {
        if (description.Length > RecipeConstraints.MaxDescriptionLength)
        {
            return new Error("RecipeDescription.MaxDescriptionLength", "Recipe description length is more than max description length constraint.");
        }
        
        if (description.Contains("<script>") || description.Contains("</script>"))
        {
            return new Error("RecipeDescription.NotAllowedTag", "Recipe description contains not allowed tag which is used by XSS.");
        }
        
        return Result.Success();
    }
}