namespace Application.Recipes.Create;

public record RecipeCreateDto(
    int AuthorId,
    string? Title,
    string? Description,
    string? Instruction,
    string ImageName,
    int? Difficulty,
    string? CookingTime,
    List<IngredientDto>? Ingredients);