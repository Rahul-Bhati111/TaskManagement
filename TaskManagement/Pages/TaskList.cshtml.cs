using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Web.Pages;

public class TaskListModel : PageModel
{
	private readonly ITaskService _taskService;
	private readonly IUserService _userService;

	public TaskListModel(
		ITaskService taskService,
		IUserService userService)
	{
		_taskService = taskService;
		_userService = userService;
	}

	public IReadOnlyList<TaskDto> Tasks { get; set; }
		= Array.Empty<TaskDto>();

	public IReadOnlyList<UserDto> Users { get; set; }
		= Array.Empty<UserDto>();

	[BindProperty(SupportsGet = true)]
	public int? AssignedUserId { get; set; }

	[BindProperty(SupportsGet = true)]
	public bool UnassignedOnly { get; set; }

	public async Task OnGetAsync()
	{
		var filter = new TaskFilterDto
		{
			AssignedUserId = AssignedUserId,
			UnassignedOnly = UnassignedOnly
		};

		Tasks = await _taskService.GetTasksAsync(filter);

		Users = await _userService.GetActiveUsersAsync();
	}
}