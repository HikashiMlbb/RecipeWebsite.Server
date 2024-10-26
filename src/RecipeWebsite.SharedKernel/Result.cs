namespace RecipeWebsite.SharedKernel;

public class Result
{
    public Error? Error { get; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result()
    {
        Error = null;
    }

    private Result(Error error)
    {
        Error = error;
    }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(Error error)
    {
        return new Result(error);
    }

    public static implicit operator Result(Error error)
    {
        return new Result(error);
    }
}