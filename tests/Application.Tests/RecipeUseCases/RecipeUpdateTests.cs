using Application.Recipes;
using Application.Recipes.GetById;
using Application.Recipes.Update;
using Application.Users.UseCases;
using Domain.RecipeEntity;
using Domain.UserEntity;
using Moq;

// ReSharper disable InconsistentNaming

namespace Application.Tests.RecipeUseCases;

public class RecipeUpdateTests
{
    private readonly Mock<IRecipeRepository> _mockRepo;
    private readonly Mock<IUserRepository> _userMock;
    private readonly RecipeUpdate _useCase;

    public RecipeUpdateTests()
    {
        _mockRepo = new Mock<IRecipeRepository>();
        _userMock = new Mock<IUserRepository>();
        _useCase = new RecipeUpdate(_mockRepo.Object, _userMock.Object);
    }

    [Fact]
    public async Task RecipeNotFound_ReturnsError()
    {
        // Arrange
        var dto = new RecipeUpdateDto(1, 2);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync((RecipeGetByIdResult)null!);

        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.RecipeNotFound, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Never);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task UserIsNotAuthor_ReturnsError()
    {
        // Arrange
        const int recipeId = 69;
        const int userId = 666;
        var returnedRecipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(70) } };
        var dto = new RecipeUpdateDto(recipeId, userId);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null))
            .ReturnsAsync(returnedRecipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });

        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.UserIsNotAuthor, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidTitle_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, "I");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.TitleLengthOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidDescription_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Description: "?");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.DescriptionLengthOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidInstruction_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Instruction: "?");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.InstructionLengthOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidDifficulty_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Difficulty: 15);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.DifficultyIsNotDefined, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidCookingTime_Format_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, CookingTime: "-1:-1:-1:-1");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.CookingTimeHasInvalidFormat, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidCookingTime_TooHuge_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, CookingTime: "7.0:0:0");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.CookingTimeIsTooHuge, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidCookingTime_TooSmall_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, CookingTime: "-1");
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeErrors.CookingTimeIsTooSmall, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidIngredientName_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Ingredients: [new IngredientDto(".", 1, "grams")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.IngredientNameLengthOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidIngredientCount_ZeroCount_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Ingredients: [new IngredientDto("egg", 0, "grams")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.IngredientCountOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidIngredientCount_TooMany_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Ingredients: [new IngredientDto("egg", 1_000_000, "Grams")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.IngredientCountOutOfRange, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task InvalidIngredient_MeasurementUnit_ReturnsError()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(13, 26, Ingredients: [new IngredientDto("egg", 1_000, "Unknown")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(RecipeDomainErrors.IngredientMeasurementUnitIsNotDefined, result.Error);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Never);
    }

    [Fact]
    public async Task FullUpdateSuccessfully_ReturnsSuccess()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(
            13,
            26,
            "ValidRecipeTitle",
            "SomeValidDescriptionSomeValidDescriptionSomeValidDescription",
            "SomeValidInstruction",
            "newImageNameGUID",
            3,
            "12:00",
            [new IngredientDto("egg", 1_000, "pieces")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Classic });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.True(result.IsSuccess);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Once);
    }
    
    [Fact]
    public async Task AdminUpdates_ReturnsSuccess()
    {
        // Arrange
        var recipe = new RecipeGetByIdResult { Author = new User { Id = new UserId(26) } };
        var dto = new RecipeUpdateDto(
            13,
            recipe.Author.Id.Value + 12345,
            "ValidRecipeTitle",
            "SomeValidDescriptionSomeValidDescriptionSomeValidDescription",
            "SomeValidInstruction",
            "newImageNameGUID",
            3,
            "12:00",
            [new IngredientDto("egg", 1_000, "pieces")]);
        _mockRepo.Setup(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null)).ReturnsAsync(recipe);
        _userMock.Setup(x => x.SearchByIdAsync(It.IsAny<UserId>())).ReturnsAsync(new User { Role = UserRole.Admin });
        
        // Act
        var result = await _useCase.UpdateAsync(dto);

        // Assert
        Assert.True(result.IsSuccess);
        _mockRepo.Verify(x => x.SearchByIdAsync(It.IsAny<RecipeId>(), null), Times.Once);
        _userMock.Verify(x => x.SearchByIdAsync(It.IsAny<UserId>()), Times.Once);
        _mockRepo.Verify(x => x.UpdateAsync(It.IsAny<RecipeUpdateConfig>()), Times.Once);
    }
}