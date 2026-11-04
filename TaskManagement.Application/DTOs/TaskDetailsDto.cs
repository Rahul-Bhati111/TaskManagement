
namespace TaskManagement.Application.DTOs;

public class TaskDetailsDto
{
	public long TaskId { get; set; }

	public string Title { get; set; } = string.Empty;

	public string? Description { get; set; }

	public DateTime DueDate { get; set; }

	public short PriorityId { get; set; }

	public string PriorityName { get; set; } = string.Empty;

	public int? AssignedUserId { get; set; }

	public string? AssignedUserName { get; set; }

	public short StatusId { get; set; }

	public string StatusName { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }
}
