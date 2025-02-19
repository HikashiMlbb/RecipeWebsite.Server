using Domain.RecipeEntity;
using Domain.UserEntity;

namespace Application.Recipes.GetById;

public class RecipeGetById
{
    private readonly IRecipeRepository _repo;

    public RecipeGetById(IRecipeRepository repo)
    {
        _repo = repo;
    }

    public async Task<RecipeGetByIdResult?> GetRecipeAsync(int recipeId, int? userId = null)
    {
        var typedRecipeId = new RecipeId(recipeId);
        var typedUserId = userId is null ? null : new UserId(userId.Value);
        var foundRecipe = await _repo.SearchByIdAsync(typedRecipeId, typedUserId);
        if (foundRecipe is null) return null;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        foundRecipe.Comments = foundRecipe.Comments is null
            ? []
            : foundRecipe.Comments.OrderByDescending(x => x.PublishedAt).ToList();
        return foundRecipe;
    }
}