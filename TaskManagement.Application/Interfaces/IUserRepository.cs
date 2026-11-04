using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IUserRepository
{
	Task<IReadOnlyList<UserDto>> GetActiveUsersAsync();
}