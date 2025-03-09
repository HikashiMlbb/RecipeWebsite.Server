using Domain.RecipeEntity;
using Domain.UserEntity;

namespace Application.Recipes.GetById;

public sealed class RecipeGetByIdResult : Recipe
{
    public Stars UserRate { get; set; }
    public bool IsModifyAllowed { get; set; }
}