
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
	Task<IReadOnlyList<TaskDto>> GetTasksAsync(
		TaskFilterDto filter);

	Task<TaskDetailsDto?> GetTaskByIdAsync(
		long taskId);

	Task<long> CreateTaskAsync(CreateTaskDto task);

	Task<EditTaskDto?> GetTaskForEditAsync(long taskId);

	Task UpdateTaskAsync(EditTaskDto task);
}
