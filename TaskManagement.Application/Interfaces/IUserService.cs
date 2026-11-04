using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IUserService
{
	Task<IReadOnlyList<UserDto>> GetActiveUsersAsync();
}