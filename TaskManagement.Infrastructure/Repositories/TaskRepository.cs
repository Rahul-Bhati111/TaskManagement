
using System.Data;
using Dapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
	private readonly IDbConnectionFactory _connectionFactory;

	public TaskRepository(IDbConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public async Task<IReadOnlyList<TaskDto>> GetAllAsync(
		TaskFilterDto filter)
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var parameters = new
		{
			filter.AssignedUserId,
			filter.UnassignedOnly
		};

		var result = await connection.QueryAsync<TaskDto>(
			"dbo.usp_Task_GetAll",
			parameters,
			commandType: CommandType.StoredProcedure
		);

		return result.ToList();
	}

	public async Task<TaskDetailsDto?> GetByIdAsync(long taskId)
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var result = await connection.QuerySingleOrDefaultAsync<TaskDetailsDto>(
			"dbo.usp_Task_GetById",
			new { TaskId = taskId },
			commandType: CommandType.StoredProcedure
		);

		return result;
	}

	public async Task<long> CreateAsync(CreateTaskDto task)
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var taskId = await connection.ExecuteScalarAsync<long>(
			"dbo.usp_Task_Create",
			new
			{
				task.Title,
				task.Description,
				task.DueDate,
				task.PriorityId,
				task.AssignedUserId
			},
			commandType: CommandType.StoredProcedure);

		return taskId;
	}

	public async Task<EditTaskDto?> GetForEditAsync(long taskId)
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		var result = await connection.QuerySingleOrDefaultAsync<EditTaskDto>(
			"dbo.usp_Task_GetById",
			new { TaskId = taskId },
			commandType: CommandType.StoredProcedure);

		return result;
	}

	public async Task UpdateAsync(EditTaskDto task)
	{
		using var connection = _connectionFactory.CreateConnection();

		await connection.OpenAsync();

		await connection.ExecuteAsync(
			"dbo.usp_Task_Update",
			new
			{
				task.TaskId,
				task.Title,
				task.Description,
				task.DueDate,
				task.PriorityId,
				task.AssignedUserId,
				task.StatusId
			},
			commandType: CommandType.StoredProcedure);
	}
}
