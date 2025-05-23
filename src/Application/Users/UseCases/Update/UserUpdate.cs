using Application.Users.Services;
using Domain.UserEntity;
using SharedKernel;

namespace Application.Users.UseCases.Update;

public class UserUpdate
{
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _repo;

    public UserUpdate(IUserRepository repo, IPasswordService passwordService)
    {
        _repo = repo;
        _passwordService = passwordService;
    }

    public async Task<Result> UpdateAsync(UserUpdateDto dto)
    {
        var userId = new UserId(dto.Id);
        var user = await _repo.SearchByIdAsync(userId);

        if (user is null) return UserErrors.UserIdNotFound;

        if (dto.OldPassword is null
            || dto.NewPassword is null
            || !await _passwordService.VerifyAsync(dto.OldPassword, user.Password))
            return UserErrors.PasswordIsIncorrect;
        
        if (dto.NewPassword.Trim().Length < Password.MinimalLength) return UserErrors.PasswordTooShort;

        var newHashedPassword = await _passwordService.CreateAsync(dto.NewPassword);
        await _repo.UpdatePasswordAsync(userId, newHashedPassword);

        return Result.Success();
    }
}