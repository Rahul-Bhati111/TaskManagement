using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Web.Pages;

public class CreateModel : PageModel
{
	private readonly ITaskService _taskService;
	private readonly IUserService _userService;
	private readonly IPriorityService _priorityService;
	private readonly ILogger<CreateModel> _logger;

	public CreateModel(
		ITaskService taskService,
		IUserService userService,
		IPriorityService priorityService,
		ILogger<CreateModel> logger)
	{
		_taskService = taskService;
		_userService = userService;
		_priorityService = priorityService;
		_logger = logger;
	}

	[BindProperty]
	public CreateTaskDto Input { get; set; } = new();

	public IReadOnlyList<UserDto> Users { get; set; }
		= Array.Empty<UserDto>();

	public IReadOnlyList<PriorityDto> Priorities { get; set; }
		= Array.Empty<PriorityDto>();

	public async Task OnGetAsync()
	{
		await LoadDropdownsAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			await LoadDropdownsAsync();
			return Page();
		}

		try
		{
			var taskId = await _taskService.CreateTaskAsync(Input);

			TempData["SuccessMessage"] =
				$"Task #{taskId} created successfully.";

			return RedirectToPage("/TaskList");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error creating task.");

			ModelState.AddModelError(
				string.Empty,
				"Unable to create task. Please verify your entries and try again.");

			await LoadDropdownsAsync();

			return Page();
		}
	}

	private async Task LoadDropdownsAsync()
	{
		Users = await _userService.GetActiveUsersAsync();

		Priorities = await _priorityService.GetActivePrioritiesAsync();
	}
}