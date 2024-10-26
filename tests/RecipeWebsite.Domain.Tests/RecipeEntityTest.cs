using RecipeWebsite.Domain.RecipeEntity;

namespace RecipeWebsite.Domain.Tests;

public class RecipeEntityTest
{
    private Rating _rating;
    
    [SetUp]
    public void Setup()
    {
        _rating = new Rating();
    }

    [Test]
    public void RecipeNameTest()
    {
        var validRecipeName = RecipeName.Create("Some valid recipe name");
        var spaceName = RecipeName.Create(new string(' ', 5000)+ "Some space recipe name");
        var bigRecipeName = RecipeName.Create(new string('$', 500));
        var scriptRecipeName = RecipeName.Create("Just a recipe name <script>alert('Hacked!')</script>");
        
        Assert.Multiple(() =>
        {
            Assert.That(validRecipeName.IsSuccess, Is.True);
            Assert.That(spaceName.IsSuccess, Is.True);
            Assert.That(bigRecipeName.IsSuccess, Is.False);
            Assert.That(scriptRecipeName.IsSuccess, Is.False);
        });
    }

    [Test]
    public void RecipeDescriptionTest()
    {
        var validRecipeDescription = RecipeDescription.Create("Some valid recipe name");
        var spaceRecipeDescription = RecipeDescription.Create(new string(' ', 5000) + "Some valid space recipe description");
        var bigRecipeDescription = RecipeDescription.Create(new string('$', 5000));
        var scriptRecipeDescription = RecipeDescription.Create("Hello! <script>alert('Hacked!')</script>");
        
        Assert.Multiple(() =>
        {
            Assert.That(validRecipeDescription.IsSuccess, Is.True);
            Assert.That(spaceRecipeDescription.IsSuccess, Is.True);
            Assert.That(bigRecipeDescription.IsSuccess, Is.False);
            Assert.That(scriptRecipeDescription.IsSuccess, Is.False);
        });       
    }

    [Test]
    public void InstructionItemTest()
    {
        var valid = InstructionItem.Create("1. Set oven to 90 degrees.");
        var invalid = InstructionItem.Create("1. Set oven to 90 degrees. <script>alert('Hacked!')</script>");
        
        Assert.Multiple(() =>
        {
            Assert.That(valid.IsSuccess, Is.True);
            Assert.That(invalid.IsSuccess, Is.False);
        });
    }

    [Test]
    public void IngredientNameTest()
    {
        var valid = IngredientName.Create("Dark chocolate");
        var spaceValid = IngredientName.Create(new string(' ', 500) + "Coconut");
        var bigInvalid = IngredientName.Create(new string('$', 500));
        var xssInvalid = IngredientName.Create("Flour <script>alert('Hacked!')</script>");
        
        Assert.Multiple(() =>
        {
            Assert.That(valid.IsSuccess, Is.True);
            Assert.That(spaceValid.IsSuccess, Is.True);
            Assert.That(bigInvalid.IsSuccess, Is.False);
            Assert.That(xssInvalid.IsSuccess, Is.False);
        });
    }
    
    [Test]
    public void RatingTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_rating.Rate, Is.EqualTo(0));
            _rating.AddRate(Stars.Five);
            Assert.That(_rating.Rate, Is.EqualTo(5));
            _rating.AddRate(Stars.One);
            Assert.That(_rating.Rate, Is.EqualTo(3));
            _rating.AddRate(Stars.One);
            Assert.That(_rating.Rate, Is.EqualTo(2));
        });
    }

    [Test]
    public void ImageLinkTest()
    {
        var validLink = ImageLink.Create("https://www.yandex.ru/image/1234432132.img");
        var validSpace = ImageLink.Create("                                https://google.com/picture?id=12345     ");
        var xssLink = ImageLink.Create("https://www.yandex.ru/image?=<script>alert('Hacked!')</script>12345.img");
        var invalidLink = ImageLink.Create("What");
        
        Assert.Multiple(() =>
        {
            Assert.That(validLink.IsSuccess, Is.True);
            Assert.That(validSpace.IsSuccess, Is.True);
            Assert.That(xssLink.IsSuccess, Is.False);
            Assert.That(invalidLink.IsSuccess, Is.False);
        });
    }
}