using Domain.RecipeEntity;
using Domain.UserEntity;

namespace Application.Recipes;

public interface IRecipeRepository
{
    public Task<RecipeId> InsertAsync(Recipe newRecipe);
    public Task<Recipe?> SearchByIdAsync(RecipeId recipeId);
    public Task RateAsync(RecipeId recipeId, UserId userId, Stars rate);
}