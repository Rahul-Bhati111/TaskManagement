using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class StatusService : IStatusService
{
	private readonly IStatusRepository _statusRepository;

	public StatusService(IStatusRepository statusRepository)
	{
		_statusRepository = statusRepository;
	}

	public Task<IReadOnlyList<StatusDto>> GetActiveStatusesAsync()
	{
		return _statusRepository.GetActiveStatusesAsync();
	}
}