using System.Data;
using Dapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class StatusRepository : IStatusRepository
{
	private readonly IDbConnectionFactory _connectionFactory;

	public StatusRepository(IDbConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<IReadOnlyList<StatusDto>> GetActiveStatusesAsync()
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var statuses = await connection.QueryAsync<StatusDto>(
			"dbo.usp_TaskStatus_GetAll",
			commandType: CommandType.StoredProcedure);

		return statuses.ToList();
	}
}