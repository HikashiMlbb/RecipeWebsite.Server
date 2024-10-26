using RecipeWebsite.SharedKernel;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.RecipeEntity;

public class IngredientName
{
    public string Value { get; init; }

    private IngredientName(string value)
    {
        Value = value;
    }

    public static Result<IngredientName> Create(string ingredientName)
    {
        ingredientName = ingredientName.Trim();
        var validateResult = Validate(ingredientName);

        if (validateResult.IsSuccess)
        {
            return new IngredientName(ingredientName);
        }

        return validateResult.Error!;
    }

    private static Result Validate(string ingredientName)
    {
        if (ingredientName.Length > RecipeConstraints.MaxIngredientNameLength)
        {
            return new Error("IngredientName.MaxLength", "Ingredient name length is more than max name length constraint.");
        }

        if (ingredientName.Contains("<script>") || ingredientName.Contains("</script>"))
        {
            return new Error("IngredientName.NotAllowedTag", "Ingredient name contains not allowed tag which is used by XSS.");
        }
        
        return Result.Success();
    }
}