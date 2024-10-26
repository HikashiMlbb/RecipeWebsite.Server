namespace RecipeWebsite.Domain.RecipeEntity;

public class Recipe
{
    public Guid Id { get; set; }
    public RecipeName Name { get; set; }
    public RecipeDescription Description { get; set; }
    public ImageLink ImageLink { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public TimeSpan CookingTime { get; set; }
    public List<InstructionItem> Instruction { get; set; }
    public Difficulty Difficulty { get; set; }
    public DateTime PublicationDate { get; set; }
    public Rating Rate { get; set; }

    public Recipe(
        Guid id, 
        RecipeName recipeName, 
        RecipeDescription recipeDescription, 
        List<Ingredient> ingredients,
        TimeSpan cookingTime,
        List<InstructionItem> instruction,
        Difficulty difficulty,
        DateTime publicationDate,
        Rating rate)
    {
        Id = id;
        Name = recipeName;
        Description = recipeDescription;
        Ingredients = ingredients;
        CookingTime = cookingTime;
        Instruction = instruction;
        Difficulty = difficulty;
        PublicationDate = publicationDate;
        Rate = rate;
    }
}