using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class PriorityService : IPriorityService
{
	private readonly IPriorityRepository _priorityRepository;

	public PriorityService(IPriorityRepository priorityRepository)
	{
		_priorityRepository = priorityRepository;
	}

	public Task<IReadOnlyList<PriorityDto>> GetActivePrioritiesAsync()
	{
		return _priorityRepository.GetActivePrioritiesAsync();
	}
}