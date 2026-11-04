
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
	private readonly ITaskRepository _taskRepository;

	public TaskService(ITaskRepository taskRepository)
	{
		_taskRepository = taskRepository
			?? throw new ArgumentNullException(nameof(taskRepository));
	}

	public async Task<IReadOnlyList<TaskDto>> GetTasksAsync(
		TaskFilterDto filter)
	{
		ArgumentNullException.ThrowIfNull(filter);

		if (filter.AssignedUserId.HasValue &&
			filter.AssignedUserId.Value <= 0)
		{
			throw new ArgumentException(
				"Invalid assigned user ID.");
		}

		if (filter.AssignedUserId.HasValue &&
			filter.UnassignedOnly)
		{
			throw new ArgumentException(
				"Assigned user and unassigned filters cannot be used together.");
		}

		return await _taskRepository.GetAllAsync(filter);
	}

	public async Task<TaskDetailsDto?> GetTaskByIdAsync(
		long taskId)
	{
		if (taskId <= 0)
		{
			throw new ArgumentException(
				"Task ID must be greater than zero.");
		}

		return await _taskRepository.GetByIdAsync(taskId);
	}

	public async Task<long> CreateTaskAsync(CreateTaskDto task)
	{
		ArgumentNullException.ThrowIfNull(task);

		if (string.IsNullOrWhiteSpace(task.Title))
			throw new ArgumentException("Task title is required.");

		if (task.Title.Length > 200)
			throw new ArgumentException("Task title cannot exceed 200 characters.");

		if (task.DueDate == default)
			throw new ArgumentException("Due date is required.");

		if (task.PriorityId <= 0)
			throw new ArgumentException("Please select a valid priority.");

		if (task.AssignedUserId.HasValue && task.AssignedUserId.Value <= 0)
			throw new ArgumentException("Invalid assigned user.");

		return await _taskRepository.CreateAsync(task);
	}

	public async Task<EditTaskDto?> GetTaskForEditAsync(long taskId)
	{
		if (taskId <= 0)
			throw new ArgumentException("Invalid task ID.");

		return await _taskRepository.GetForEditAsync(taskId);
	}

	public async Task UpdateTaskAsync(EditTaskDto task)
	{
		ArgumentNullException.ThrowIfNull(task);

		if (task.TaskId <= 0)
			throw new ArgumentException("Invalid task ID.");

		if (string.IsNullOrWhiteSpace(task.Title))
			throw new ArgumentException("Task title is required.");

		if (task.Title.Length > 200)
			throw new ArgumentException("Task title cannot exceed 200 characters.");

		if (task.DueDate == default)
			throw new ArgumentException("Due date is required.");

		if (task.PriorityId <= 0)
			throw new ArgumentException("Please select a valid priority.");

		if (task.StatusId <= 0)
			throw new ArgumentException("Please select a valid status.");

		if (task.AssignedUserId.HasValue && task.AssignedUserId.Value <= 0)
			throw new ArgumentException("Invalid assigned user.");

		await _taskRepository.UpdateAsync(task);
	}
}
