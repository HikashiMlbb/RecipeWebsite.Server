using RecipeWebsite.SharedKernel;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.CommentEntity;

public class CommentContent
{
    public string Value { get; init; }

    private CommentContent(string value)
    {
        Value = value;
    }

    public static Result<CommentContent> Create(string value)
    {
        var resultValidation = Validate(value);
        if (resultValidation.IsSuccess)
        {
            return new CommentContent(value);
        }

        return resultValidation.Error!;
    }

    private static Result Validate(string value)
    {
        if (value.Length > CommentConstraints.MaxContentLength)
        {
            return new Error("Comment.MaxContentLength", "Comment content length is more than max content length constraint.");
        }

        if (value.Contains("<script>") || value.Contains("</script>"))
        {
            return new Error("Comment.UnallowedTag", "Comment content contains unallowed tag which is used for XSS.");
        }
        
        return Result.Success();
    }
}