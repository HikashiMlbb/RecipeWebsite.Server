namespace RecipeWebsite.Domain.RecipeEntity;

public class Ingredient(IngredientName name, int amount, AmountType amountType)
{
    public IngredientName Name { get; set; } = name;
    public int Amount { get; set; } = amount;
    public AmountType AmountType { get; set; } = amountType;
}