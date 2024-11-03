using System.Text.RegularExpressions;
using RecipeWebsite.SharedKernel;

namespace RecipeWebsite.Domain.RecipeEntity;

public partial class ImageLink
{
    public string Value { get; init; }

    private ImageLink(string value)
    {
        Value = value;
    }

    public static Result<ImageLink> Create(string link)
    {
        link = link.Trim();
        var validateResult = Validate(link);

        if (validateResult.IsSuccess)
        {
            return new ImageLink(link);
        }

        return validateResult.Error!;
    }

    private static Result Validate(string link)
    {
        var regex = LinkRegex();
        if (!regex.IsMatch(link) || link.Contains("<script>") || link.Contains("</script>"))
        {
            return new Error("ImageLink.Invalid", "Image link format is invalid.");
        }
        
        return Result.Success();
    }

    [GeneratedRegex(@"https?://.+\..+/.+")]
    private static partial Regex LinkRegex();
}