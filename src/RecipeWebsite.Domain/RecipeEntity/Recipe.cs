using RecipeWebsite.Domain.CommentEntity;

namespace RecipeWebsite.Domain.RecipeEntity;

public class Recipe
{
    public RecipeId Id { get; set; }
    public RecipeName Name { get; set; }
    public RecipeDescription Description { get; set; }
    public Guid ImageName { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public TimeSpan CookingTime { get; set; }
    public List<InstructionItem> Instruction { get; set; }
    public Difficulty Difficulty { get; set; }
    public DateTime PublicationDate { get; set; }
    public Rating Rate { get; set; }
    public List<CommentId> CommentsId { get; set; }

    public Recipe(
        RecipeId id, 
        RecipeName recipeName, 
        RecipeDescription recipeDescription,
        Guid imageName, 
        List<Ingredient> ingredients,
        TimeSpan cookingTime,
        List<InstructionItem> instruction,
        Difficulty difficulty,
        DateTime publicationDate,
        Rating rate,
        List<CommentId> commentsId)
    {
        Id = id;
        Name = recipeName;
        Description = recipeDescription;
        ImageName = imageName;
        Ingredients = ingredients;
        CookingTime = cookingTime;
        Instruction = instruction;
        Difficulty = difficulty;
        PublicationDate = publicationDate;
        Rate = rate;
        CommentsId = commentsId;
    }
}