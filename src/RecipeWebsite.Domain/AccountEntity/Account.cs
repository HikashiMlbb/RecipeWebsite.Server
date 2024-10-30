using RecipeWebsite.Domain.RecipeEntity;

namespace RecipeWebsite.Domain.AccountEntity;

public class Account
{
    public AccountId Id { get; set; }
    public Email Email { get; set; }
    public Nickname Nickname { get; set; }
    public Password Password { get; set; }
    public List<RecipeId> RecipesId { get; set; }

    public Account(
        AccountId id,
        Email email,
        Nickname nickname,
        Password password,
        List<RecipeId> recipesId)
    {
        Id = id;
        Email = email;
        Nickname = nickname;
        Password = password;
        RecipesId = recipesId;
    }
}