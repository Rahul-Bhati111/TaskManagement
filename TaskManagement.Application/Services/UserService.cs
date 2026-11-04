using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public Task<IReadOnlyList<UserDto>> GetActiveUsersAsync()
	{
		return _userRepository.GetActiveUsersAsync();
	}
}