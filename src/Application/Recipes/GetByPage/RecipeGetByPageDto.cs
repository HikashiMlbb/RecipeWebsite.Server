namespace Application.Recipes.GetByPage;

public record RecipeGetByPageDto(int Page, int PageSize, string SortType = "popular");