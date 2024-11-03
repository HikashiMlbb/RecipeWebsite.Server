using RecipeWebsite.Domain.AccountEntity;
using RecipeWebsite.Domain.RecipeEntity;

namespace RecipeWebsite.Domain.CommentEntity;

public class Comment
{
    public CommentId Id { get; set; }
    public RecipeId RecipeId { get; set; }
    public AccountId AuthorId { get; set; }
    public CommentId? ReplyId { get; set; }
    public CommentContent Content { get; set; }
    public DateTime PublishedAt { get; set; }

    public Comment(
        CommentId id,
        RecipeId recipeId,
        AccountId authorId,
        CommentContent content,
        DateTime publishedAt,
        CommentId? replyId = null)
    {
        Id = id;
        RecipeId = recipeId;
        AuthorId = authorId;
        Content = content;
        PublishedAt = publishedAt;
        ReplyId = replyId;
    }
}