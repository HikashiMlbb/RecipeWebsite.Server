using Application.Users.Services;
using Domain.UserEntity;

namespace Infrastructure.Services;

public class UserPrivilegeService(string? adminUsername) : IUserPrivilegeService
{
    public bool IsAdminUsername(Username username)
    {
        return username.Value == adminUsername;
    }
}