using Domain.RecipeEntity;
using Domain.UserEntity;

namespace Application.Recipes.GetById;

public sealed class RecipeGetByIdResult
{
    public RecipeId Id { get; set; } = null!;
    public User Author { get; set; } = null!;
    public RecipeTitle Title { get; set; } = null!;
    public RecipeDescription Description { get; set; } = null!;
    public RecipeInstruction Instruction { get; set; } = null!;
    public RecipeImageName ImageName { get; set; } = null!;
    public RecipeDifficulty Difficulty { get; set; }
    public DateTimeOffset PublishedAt { get; set; }
    public TimeSpan CookingTime { get; set; }
    public Stars UserRate { get; set; }

    public Domain.RecipeEntity.Rate Rate { get; set; } = null!;
    public ICollection<Ingredient> Ingredients { get; set; } = null!;
    public ICollection<Domain.RecipeEntity.Comment> Comments { get; set; } = null!;
}