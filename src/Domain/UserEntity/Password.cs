namespace Domain.UserEntity;

public sealed record Password(string PasswordHash)
{
    public const int MinimalLength = 6;
}