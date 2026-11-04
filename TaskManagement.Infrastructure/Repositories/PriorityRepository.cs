using System.Data;
using Dapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class PriorityRepository : IPriorityRepository
{
	private readonly IDbConnectionFactory _connectionFactory;

	public PriorityRepository(IDbConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<IReadOnlyList<PriorityDto>> GetActivePrioritiesAsync()
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var priorities = await connection.QueryAsync<PriorityDto>(
			"dbo.usp_TaskPriority_GetAll",
			commandType: CommandType.StoredProcedure);

		return priorities.ToList();
	}
}