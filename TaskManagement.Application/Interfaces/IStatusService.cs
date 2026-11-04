using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IStatusService
{
	Task<IReadOnlyList<StatusDto>> GetActiveStatusesAsync();
}