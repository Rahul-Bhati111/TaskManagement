using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Web.Pages;

public class DetailsModel : PageModel
{
	private readonly ITaskService _taskService;
	private readonly IUserService _userService;
	private readonly IPriorityService _priorityService;
	private readonly IStatusService _statusService;
	private readonly ILogger<DetailsModel> _logger;

	public DetailsModel(
		ITaskService taskService,
		IUserService userService,
		IPriorityService priorityService,
		IStatusService statusService,
		ILogger<DetailsModel> logger)
	{
		_taskService = taskService;
		_userService = userService;
		_priorityService = priorityService;
		_statusService = statusService;
		_logger = logger;
	}

	[BindProperty]
	public EditTaskDto Input { get; set; } = new();

	public IReadOnlyList<UserDto> Users { get; set; }
		= Array.Empty<UserDto>();

	public IReadOnlyList<PriorityDto> Priorities { get; set; }
		= Array.Empty<PriorityDto>();

	public IReadOnlyList<StatusDto> Statuses { get; set; }
		= Array.Empty<StatusDto>();

	public async Task<IActionResult> OnGetAsync(long id)
	{
		var task = await _taskService.GetTaskForEditAsync(id);

		if (task == null)
			return NotFound();

		Input = task;

		await LoadDropdownsAsync();

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(long id)
	{
		if (id != Input.TaskId)
			return BadRequest();

		if (!ModelState.IsValid)
		{
			await LoadDropdownsAsync();
			return Page();
		}

		try
		{
			await _taskService.UpdateTaskAsync(Input);

			TempData["SuccessMessage"] =
				$"Task #{Input.TaskId} updated successfully.";

			return RedirectToPage("/TaskList");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error updating task {TaskId}", Input.TaskId);

			ModelState.AddModelError(
				string.Empty,
				"Unable to update task. Please verify your entries and try again.");

			await LoadDropdownsAsync();

			return Page();
		}
	}

	private async Task LoadDropdownsAsync()
	{
		Users = await _userService.GetActiveUsersAsync();

		Priorities = await _priorityService.GetActivePrioritiesAsync();

		Statuses = await _statusService.GetActiveStatusesAsync();
	}
}