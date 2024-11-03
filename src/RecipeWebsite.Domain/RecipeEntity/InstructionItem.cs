using RecipeWebsite.SharedKernel;

namespace RecipeWebsite.Domain.RecipeEntity;

public class InstructionItem
{
    public string Value { get; init; }

    private InstructionItem(string value)
    {
        Value = value;
    }
    
    public static Result<InstructionItem> Create(string instructionItem)
    {
        instructionItem = instructionItem.Trim();
        var validateResult = Validate(instructionItem);

        if (validateResult.IsSuccess)
        {
            return new InstructionItem(instructionItem);
        }

        return validateResult.Error!;
    }

    private static Result Validate(string instructionItem)
    {
        if (instructionItem.Contains("<script>") || instructionItem.Contains("</script>"))
        {
            return new Error("InstructionItem.NotAllowedTag", "Instruction item contains not allowed tag which is used for XSS.");
        }
        
        return Result.Success();
    }
}