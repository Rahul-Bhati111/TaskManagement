using System.Data;
using Dapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly IDbConnectionFactory _connectionFactory;

	public UserRepository(IDbConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<IReadOnlyList<UserDto>> GetActiveUsersAsync()
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var users = await connection.QueryAsync<UserDto>(
			"dbo.usp_User_GetActiveUsers",
			commandType: CommandType.StoredProcedure);

		return users.ToList();
	}
}