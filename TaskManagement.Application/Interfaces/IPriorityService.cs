using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IPriorityService
{
	Task<IReadOnlyList<PriorityDto>> GetActivePrioritiesAsync();
}