
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskRepository
{
	Task<IReadOnlyList<TaskDto>> GetAllAsync(
		TaskFilterDto filter);

	Task<TaskDetailsDto?> GetByIdAsync(
		long taskId);

	Task<long> CreateAsync(CreateTaskDto task);

	Task<EditTaskDto?> GetForEditAsync(long taskId);

	Task UpdateAsync(EditTaskDto task);
}
