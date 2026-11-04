using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IStatusRepository
{
	Task<IReadOnlyList<StatusDto>> GetActiveStatusesAsync();
}