
namespace TaskManagement.Application.DTOs;

public class TaskDto
{
	public long TaskId { get; set; }

	public string Title { get; set; } = string.Empty;

	public DateTime DueDate { get; set; }

	public string PriorityName { get; set; } = string.Empty;

	public int? AssignedUserId { get; set; }

	public string? AssignedUserName { get; set; }

	public string StatusName { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; }
}
