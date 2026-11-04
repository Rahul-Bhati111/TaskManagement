using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface IPriorityRepository
{
	Task<IReadOnlyList<PriorityDto>> GetActivePrioritiesAsync();
}